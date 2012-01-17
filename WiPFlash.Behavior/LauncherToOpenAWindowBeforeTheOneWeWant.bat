echo "Launching petshop..."

REM The hack to get round sleep being in ms in Windows XP and secs in Windows 7.
REM Thank you StackOverflow
REM http://stackoverflow.com/questions/1672338/how-to-sleep-for-5-seconds-in-windowss-command-prompt-or-dos
ping 1.1.1.1 -n 1 -w 1000 > nul

..\..\..\Example.Petshop\bin\debug\Example.Petshop.exe