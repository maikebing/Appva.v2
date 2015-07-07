# Appva.Cryptography

### Password Hashing
```c#
var random = Password.Random();
var hashed = Password.Pbkdf2(original);
Password.Equals(random, hashed);
```
### Hashing
```c#
Hash.Using<MurmurHash>("foo").Build();
Hash.Random();
Checksum.Using<MurmurHash>("foo").Build();
Checksum.Assert<MurmurHash>("foo").Equals("bar");
```

### Create CA Certificate
```c#
Certificate.CertificateAuthority().Subject("MyTrustedCARoot")
	.Use(Cipher.Ecdh(Curve.P384)).Signature(Signature.Sha512WithEcdsa)
	.WriteToDisk("C:\\cacert.pfx", "password");
```

### Create Client Certificate
```c#
Certificate.Client("MyTrustedCARoot").Subject("MyClientCertificate")
	.Use(Cipher.Ecdh(Curve.P384)).Signature(Signature.Sha512WithEcdsa)
	.WriteToDisk("C:\\clientcert.pfx", "password");
```

### Find By Subject Distinguished Name
```c#
Certificate.FindBySubjectDistinguishedName("...");
```

### Find By Thumbprint
```c#
Certificate.FindByThumbprint("...");
```

### Find By Serial Number
```c#
Certificate.FindBySerialNumber("...");
```