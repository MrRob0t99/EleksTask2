
>TourServer.EmailService.SendEmailAsync(string, string, string)F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs
 (	emailsubjectmessage"0*∏
0ec
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (0
%0"MimeKit.MimeMessage{
y
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs# (.
%1"!MimeKit.MimeMessage.MimeMessage()*

%0h
f
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (0
emailMessage"__id*

%0Ä
~
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (
%2"MimeKit.MimeMessage.From.get*

emailMessagehf
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs" (i
%3"MimeKit.MailboxAddress†
ù
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs& (4
%4"5MimeKit.MailboxAddress.MailboxAddress(string, string)*

%3*
""*
""õ
ò
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (j
%5"8MimeKit.InternetAddressList.Add(MimeKit.InternetAddress)*

%2*

%3~
|
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (
%6"MimeKit.MimeMessage.To.get*

emailMessagehf
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs  (=
%7"MimeKit.MailboxAddress£
†
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs$ (2
%8"5MimeKit.MailboxAddress.MailboxAddress(string, string)*

%7*
""*	

emailõ
ò
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (>
%9"8MimeKit.InternetAddressList.Add(MimeKit.InternetAddress)*

%6*

%7í
è
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (*
%10"MimeKit.MimeMessage.Subject.set*

emailMessage*
	
subject~
|
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs- (I
%11"__id*%*#
MimeKit.Text.TextFormat"
Htmlca
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs  (
%12"MimeKit.TextPartò
ï
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs$ (,
%13"2MimeKit.TextPart.TextPart(MimeKit.Text.TextFormat)*

%12*

%11É
Ä
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (
%14"MimeKit.TextPart.Text.set*

%12*
	
messageã
à
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (
%15"MimeKit.MimeMessage.Body.set*

emailMessage*

%12nl
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs  (0
%16"MailKit.Net.Smtp.SmtpClientÖ
Ç
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs$ (.
%17"(MailKit.Net.Smtp.SmtpClient.SmtpClient()*

%16c
a
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (0
client"__id*

%16*
1*ˆ
1◊
‘
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (F
%18"WMailKit.MailService.ConnectAsync(string, int, bool, System.Threading.CancellationToken)*


client*
""*
""*
""*
""—
Œ
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (X
%19"YMailKit.MailService.AuthenticateAsync(string, string, System.Threading.CancellationToken)*


client*
""*
""*
""ı
Ú
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (4
%20"sMailKit.MailTransport.SendAsync(MimeKit.MimeMessage, System.Threading.CancellationToken, MailKit.ITransferProgress)*


client*

emailMessage*
""*
""≈
¬
F
<C:\Users\ikise\EleksProject\Task2\EleksTask\EmailServices.cs (2
%21"UMailKit.Net.Smtp.SmtpClient.DisconnectAsync(bool, System.Threading.CancellationToken)*


client*
""*
""*
2*
2"
""