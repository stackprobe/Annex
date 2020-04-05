COPY C:\Dev\wb\t20200330_Žž•ñ\media\jihou-sine-3f.wav C:\temp\jho.wav
COPY C:\var\mp4\mp4\ddd.wav C:\temp\.
COPY C:\var\mp4\mp4\hbn.wav C:\temp\.
COPY C:\var\mp4\mp4\rlg.wav C:\temp\.

wav2csv.exe C:\temp\jho.wav C:\temp\jho.csv C:\temp\jho.hz
wav2csv.exe C:\temp\rlg.wav C:\temp\rlg.csv C:\temp\rlg.hz
wav2csv.exe C:\temp\hbn.wav C:\temp\hbn.csv C:\temp\hbn.hz
wav2csv.exe C:\temp\ddd.wav C:\temp\ddd.csv C:\temp\ddd.hz
