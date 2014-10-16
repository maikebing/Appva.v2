# Appva.Core

#### String Extensions
```c#
"foo".IsEmpty();
"bar".IsNotEmpty();
"{0}-{1}".FormatWith("foo", "bar");
"this.will.be.show.but.not.me".StripLast('.');
"foo".FirstToUpper();
"3adcee2a-d0ed-4ee4-9375-87e1f8d443dc".ToGuid();
"foo".ToUtf8Bytes();
"-*.\@".StripInvalidFileNameCharacters();
"foo".ToHex();
"666f6f".FromHex();
"FooBar".ToUrlFriendly();
"FooBar".ToLowerCaseUnderScore();
"Zm9vYmFy".FromBase64();
```

#### Guid Extensions
```c#
new Guid("00000000-0000-0000-0000-000000000000").IsEmpty();
new Guid("00000000-0000-0000-0000-000000000000").IsNotEmpty();
```

### Messaging
#### Wiring it up with AutoFac
```c#
var builder = new ContainerBuilder();
builder.Register<EmailService>(x => new EmailService(...)).As<IEmailService>().SingleInstance();
builder.Register<SmsService>(x => new SmsService(...)).As<ISmsService>().SingleInstance();
DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
```

#### Stand-alone E-mail
```c#
var emailService = new EmailService();
emailService.Send(new EmailMessage("to@example.com", "subject", "<h1>Hello World</h1>"));
await emailService.SendAsync(new EmailMessage("to@example.com", "subject", "<h1>Hello Async World</h1>"));
```

#### Stand-alone SMS
```c#
var smsService = new SmsService("id", "user", "password", "sender");
smsService.Send(new SmsMessage("toCellPhoneNumber", "Hello"));
await smsService.SendAsync(new SmsMessage("toCellPhoneNumber", "Hello Async"));
```