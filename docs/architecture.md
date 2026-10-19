# Architecture

**Status: APPROVED — Laptop Returns Demo.**

## Approved project-specific design

- Use .NET 10 and the existing ASP.NET Core project.
- Provide three simple HTTP endpoints. Simulate staff confirmation through a separate HTTP endpoint.
- Use in-memory storage with one predefined demonstration laptop.
- Use automated tests to verify the critical business rule in [requirements.md](requirements.md).

## Technology or design constraints

- No database.
- No authentication or authorization system.
- Preserve the approved return rules: reporting a return changes CheckedOut to ReturnReported and must never make the laptop Available. Only separate staff confirmation of physical receipt changes ReturnReported to Available. A laptop in ReturnReported cannot be issued.
