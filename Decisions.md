# DECISIONS.md

## 1. What did AI generate vs. what I wrote/modified myself

I designed the entities, the Architecture layering (Domain/Application/Infrastructure/API),
and the DbContext/migrations myself. I used AI heavily to scaffold repetitive parts once the
structure existed — DTOs, controller actions, service methods, specifications, and AutoMapper
profiles — and especially to debug runtime errors as they came up (EF Core FK violations,
AutoMapper exceptions, CORS, missing eager-loading).

I reviewed and tested every suggestion before accepting it rather than pasting blindly — several
times I asked "why is this happening" instead of just "fix this," so I understood the cause
before applying a change. For example, a foreign key seeding failure traced back to identity ID
drift (hardcoded IDs in seed JSON no longer matching the database after a partial reseed), and
a `dspName` field coming back null traced back to my specification class only supporting
single-level `Include` calls, with no way to eager-load a nested navigation like
`TrackDistributions.Dsp` — fixed by adding string-based include support instead of assuming
`ThenInclude` would work.

## 2. Security issues found in AI-generated code

- The first version of the create-track endpoint reused a single `TrackDto` for both request
  input and response output. This meant the create endpoint had no way to receive `Isrc`/`Title`
  at all (causing a NULL constraint failure), and more importantly, mixing input and output
  models is a bad practice — it risks exposing or accepting fields that shouldn't cross that
  boundary. Fixed by splitting into a dedicated create DTO and a separate read DTO.
- Sending an invalid `Status` value caused an unhandled `AutoMapperMappingException` to bubble
  up as a raw 500 with a full internal stack trace exposed to the client — an information
  disclosure issue. Fixed by validating with `Enum.TryParse` up front and returning a clean
  400 instead of letting the exception propagate.
- `Isrc` had no uniqueness constraint at the database level even though the spec requires it
  to be a unique identifier. Fixed by adding a unique index via EF configuration and a migration,
  rather than relying only on an application-level check that could race under concurrent requests.
- CORS was initially wide open during debugging; I locked it down to the specific frontend
  origin (`http://localhost:4200`) rather than `AllowAnyOrigin()`.

## 3. One thing AI got wrong that I had to fix

Seed data used hardcoded numeric IDs (e.g. `artistId: 1-4`, `dspId: 1-3`), assuming the
database would always be empty and identities would start at 1. Once the `Artists` table
already had data from an earlier seed attempt, the real IDs had shifted (e.g. to 5-8), and
every subsequent seed file kept failing on foreign key constraints because the AI-suggested
JSON didn't account for identity drift. This was wrong because auto-increment integer IDs
are not a stable reference across environments or reseeds — the same seed file can point at
different rows depending on what already exists in the database. I fixed it for this task by
querying the actual IDs and remapping the JSON manually. The more correct long-term fix —
which I'd apply in a real project — is to use GUIDs as primary keys instead of auto-increment
integers. A GUID can be generated up front and hardcoded into the seed JSON itself (or
generated deterministically from a stable value like the ISRC), so the identifier never
depends on insertion order or what's already in the table — the same seed file works
correctly whether the database is empty, partially seeded, or reset.
