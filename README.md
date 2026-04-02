# MartPOS (Foundation)

This repository now contains a **production-oriented foundation** for the requested retail POS architecture in C# with a layered structure:

- `MartPOS.UI` (desktop UI shell)
- `MartPOS.Core` (models, service contracts, business services)
- `MartPOS.Data` (EF Core `AppDbContext`)
- `MartPOS.Common` (enums/constants)

## Important status

You asked for a complete enterprise POS covering 10 modules, hardware integrations, reports, migrations, and full WinForms/WPF UI implementation.
That scope is **much larger than a single pass** and should be delivered iteratively.

This commit provides:

1. Core domain entities for all requested tables.
2. Common enums/constants.
3. Sales service with invoice generation and tax calculations.
4. EF Core DbContext with all DbSets and initial constraints.
5. Basic DI-driven UI entry point and a working Sales form skeleton.
6. `appsettings.json` template and Windows install batch script.

## Run

> Requires .NET 8 SDK and Windows for WinForms execution.

```bash
# from repo root
# dotnet restore
# dotnet run --project MartPOS.UI
```

## Default credentials target

- Username: `admin`
- Password: `Admin@1234`

(Seeding workflow is planned for the next iteration.)

## Suggested phased delivery

1. **Phase 1:** Full data layer + migrations + seed + authentication
2. **Phase 2:** Sales screen + barcode + receipt printing + shift controls
3. **Phase 3:** Inventory/PO/customer/employee modules
4. **Phase 4:** Reporting dashboard + exports + hardware hardening
5. **Phase 5:** Installer polish, QA, and deployment docs
