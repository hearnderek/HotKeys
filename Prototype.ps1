Add-Type -AssemblyName System.Windows.Forms

Add-Type @"
    using System;
    using System.Runtime.InteropServices;
    public class Window {

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    }

    public struct RECT
    {
    public int Left;        // x position of upper-left corner
    public int Top;         // y position of upper-left corner
    public int Right;       // x position of lower-right corner
    public int Bottom;      // y position of lower-right corner
    }

"@

[void][System.Reflection.Assembly]::LoadWithPartialName('Microsoft.VisualBasic')



$logfile = "C:\Users\derek.hearn\cmdtools\logs\Timespent.log"

$i = 10
$ctrl = 0
$alt = 0
$shiftctrl = 0
$dance = $false
while ($true -or $i -gt 0){

    if([System.Windows.Forms.Control]::ModifierKeys -eq "Control"){
        $ctrl += 1
        if($ctrl -eq 5){
            #start-process powershell
            $now = [DateTime]::Now.ToString("yyyy-MM-dd hh:mm:ss")
            $response = [Microsoft.VisualBasic.Interaction]::InputBox("What are you working on?", "Time Logger", "")
            "$now - $response"| Out-File $logfile -Encoding utf8 -Append
        }
    }
    else {
        $ctrl = 0
    }


    if([System.Windows.Forms.Control]::ModifierKeys -eq "Alt"){
        $alt += 1
        if($alt -eq 5){
            $mainwindowhandle = [Window]::GetForegroundWindow()

            $process = get-process | ? { $_.mainwindowhandle -eq $mainwindowhandle }
            $winHandle = $process.MainWindowHandle
            $rect = New-Object RECT

            [Window]::GetWindowRect($winHandle,[ref]$rect)

            # Get the middle of the screen
            [int] $mHight = ($rect.Bottom - $rect.Top)/2 + $rect.Top
            [int] $mWidth = ($rect.Right - $rect.Left)/2 + $rect.Left

            # For some reason we can set position via string
            [System.Windows.Forms.Cursor]::Position = "$mWidth,$mHight" 
        }
    }
    else {
        $alt = 0
    }


    if([System.Windows.Forms.Control]::ModifierKeys -eq "Shift, Control"){
        $shiftctrl += 1
        if($shiftctrl -eq 5){
            $dance = ! $dance
            "Dance!"
        }
    }
    else {
        $shiftctrl = 0
    }

    sleep -Milliseconds 200
    $i -= 1
}
