using System.IO;
using System.Text.Json;
using System.Windows;
namespace OpenFix;
public partial class MainWindow : Window
{
    private IReadOnlyList<Finding> findings = Array.Empty<Finding>();
    public MainWindow() { InitializeComponent(); if (ScanButton.Parent is System.Windows.Controls.Panel actions) { var activation = new System.Windows.Controls.Button { Content = "Fix activation", ToolTip = "Open Microsoft's activation and troubleshooting page" }; activation.Click += Activation_Click; actions.Children.Insert(1, activation); } Opacity = 0; Loaded += (_, _) => BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(260))); }
    private void Activation_Click(object sender, RoutedEventArgs e) { try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("ms-settings:activation") { UseShellExecute = true }); } catch (Exception ex) { EvidenceText.Text = $"Could not open Activation settings: {ex.Message}"; } }
    private async void Scan_Click(object sender, RoutedEventArgs e) { ScanButton.IsEnabled = false; SummaryText.Text = "Inspecting system…"; EvidenceText.Text = "Reading evidence without changing settings."; try { findings = await Task.Run(OpenFixEngine.Scan); FindingsList.ItemsSource = findings.Select(f => $"{(f.Level == FindingLevel.Good ? "✓" : "!")}  {f.Title}\n    {f.Explanation}\n    Evidence: {f.Evidence}").ToArray(); var attention = findings.Count(f => f.Level != FindingLevel.Good); SummaryText.Text = attention == 0 ? "Your system looks healthy" : $"{attention} item(s) need review"; CountText.Text = $"{findings.Count} checks · {attention} requiring attention"; EvidenceText.Text = "Scan complete · no changes were applied."; ReportButton.IsEnabled = true; } catch (Exception ex) { SummaryText.Text = "Scan could not complete"; EvidenceText.Text = ex.Message; } finally { ScanButton.IsEnabled = true; } }
    private void Report_Click(object sender, RoutedEventArgs e) { if (findings.Count == 0) return; using var dialog = new System.Windows.Forms.SaveFileDialog { Filter = "OpenFix report (*.json)|*.json", FileName = "openfix-report.json" }; if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) File.WriteAllText(dialog.FileName, JsonSerializer.Serialize(findings, new JsonSerializerOptions { WriteIndented = true })); }
}
