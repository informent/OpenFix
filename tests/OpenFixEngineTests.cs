using OpenFix;
var findings = OpenFixEngine.Scan();
if (findings.Count < 4) throw new Exception("Expected core system checks.");
if (!findings.Any(f => f.Area == "Safety" && f.Level == FindingLevel.Good)) throw new Exception("Read-only safety finding missing.");
Console.WriteLine($"PASS: OpenFix scan returned {findings.Count} checks");
