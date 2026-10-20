# Alignment Demo

**A working .NET 10 demonstration of the Alignment approach to AI-assisted software development.**

This application was generated from the [Alignment Toolkit](https://github.com/IrishChieftain/alignment-toolkit).

Its purpose is to demonstrate how AI coding assistants can work with approved project knowledge stored in Git, while a human developer retains responsibility for correctness.

## The demonstration

The application implements a small laptop checkout and return workflow.

A laptop has three possible states:

`Available → CheckedOut → ReturnReported`

The important business rule is that **reporting a laptop as returned must not make it available for another employee**.

A separate staff receipt-confirmation operation is required to return it to `Available`.

The application exposes three HTTP endpoints:

| Endpoint | Action |
|---|---|
| `POST /laptops/demo/checkout` | Check out an available laptop |
| `POST /laptops/demo/report-return` | Report a checked-out laptop as returned |
| `POST /laptops/demo/confirm-receipt` | Confirm physical receipt and make the laptop available |

Valid transitions return HTTP 200. Invalid transitions return HTTP 409 without changing the laptop's state.

The demonstration uses a single in-memory laptop. There is no database, authentication, or user interface.

## Build and run

**Requirement:** .NET 10 SDK.

Clone the repository:

```powershell
git clone https://github.com/IrishChieftain/alignment-demo.git
cd alignment-demo
```

Build the solution:

```powershell
dotnet build .\AlignmentDemo.slnx
```

Run the application:

```powershell
dotnet run --project .\AlignmentDemo\AlignmentDemo.csproj
```

Run the automated tests:

```powershell
dotnet test .\AlignmentDemo.slnx
```

The implementation was verified with **eight passing automated tests** during the original experiment.

## What the experiment demonstrated

The implementation followed a human-reviewed workflow:

1. **Requirements and architecture were documented and approved** in the repository.
2. A fresh GitHub Copilot conversation recovered those requirements from the Markdown files without having them pasted into the conversation.
3. Copilot implemented the application and automated tests.
4. An initial six tests passed.
5. ChatGPT Browser reviewed the actual C# implementation and identified a potential concurrency problem involving shared state.
6. Copilot corrected the state transition logic using synchronization.
7. The solution built successfully, and eight tests passed.
8. The developer committed the implementation and approved verification evidence to Git.

The concurrency risk was identified through code review, **not reproduced using concurrent test requests**. The two additional tests covered repeated invalid operations.

This is evidence of a successful small-scale exercise, not proof that Alignment consistently produces better software.

## Explore the project knowledge

The `docs/` directory contains the knowledge used during development:

- `docs/index.md` — documentation entry point.
- `docs/requirements.md` — approved business rules and boundaries.
- `docs/architecture.md` — approved implementation approach.
- `docs/decisions.md` — project decision record.
- `docs/changes/2026-10-19-laptop-returns-demo-verification.md` — verification evidence from the completed exercise.
- `docs/Alignment Evidence.base` — Obsidian Bases view of the verification records.

The `.github/copilot-instructions.md` file tells Copilot where to find project knowledge.

**The Git repository is the source of truth.** ChatGPT Work Desktop maintains approved Markdown knowledge; Obsidian provides a convenient way to read and explore the same files without maintaining a separate copy.

The developer reviews and approves changes before committing them.

## What this does — and doesn't — prove

The demonstration shows that an AI coding assistant can:

- Recover documented project requirements in a fresh conversation.
- Implement functionality using those requirements.
- Respond to an identified code-review issue.
- Produce results that can be linked to versioned verification evidence.

It does **not** establish measurable improvements in development speed, token consumption, defect rates, or overall productivity.

Those questions remain part of the continuing Alignment experiment.

## Start your own project

Use the **[Alignment Toolkit](https://github.com/IrishChieftain/alignment-toolkit)** to generate a new application with the same starting knowledge structure.

This repository is the worked example; the Toolkit is the reusable starting point.
