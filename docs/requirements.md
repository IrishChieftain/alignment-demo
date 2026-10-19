# Approved requirements

**Status: APPROVED — Laptop Returns Demo.**

## Goal

Track laptop returns so that reporting a return does not make the laptop available for issue. Only a separate staff confirmation of physical receipt can make it Available.

## Required behavior

The three relevant states are:

- **Available** — the laptop can be issued.
- **CheckedOut** — the laptop is issued to an employee.
- **ReturnReported** — the employee reports returning it, but staff have not confirmed receipt.

Required transitions:

- **CheckedOut → ReturnReported:** the employee reports the return.
- **ReturnReported → Available:** staff confirms physical receipt.

## Prohibited behavior / exclusions

Reporting a return must not make the laptop Available. A laptop in ReturnReported cannot be issued to another employee.

## Acceptance criteria

1. Reporting a return changes CheckedOut to ReturnReported.
2. Reporting a return never makes the laptop Available.
3. Only staff confirmation changes ReturnReported to Available.
4. A laptop in ReturnReported cannot be issued to another employee.
