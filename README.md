# OpenFix

OpenFix is a read-only-first Windows health and troubleshooting tool.

It scans for useful evidence—system-drive space, temporary-file growth, network availability, startup inventory, and scan safety—then explains what it found in plain language. Reports can be exported for review or support.

## Activation

OpenFix includes a **Fix activation** action that opens Windows' official Activation settings. It does not bypass licensing, hide activation notices, or modify protected licensing components.

## Principles

- Scan before changing anything
- Explain evidence and impact
- Preview repairs before applying them
- Keep changes reversible
- No account, telemetry, or cloud service required

## Build and test

```powershell
dotnet run --project tests/OpenFixEngineTests.csproj
dotnet build OpenFix.csproj -c Release
dotnet publish OpenFix.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

OpenFix is not affiliated with Microsoft or Windows.
