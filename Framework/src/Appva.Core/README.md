# Appva.Core

### String Extensions
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

### Guid Extensions
```c#
new Guid("00000000-0000-0000-0000-000000000000").IsEmpty();
new Guid("00000000-0000-0000-0000-000000000000").IsNotEmpty();
```