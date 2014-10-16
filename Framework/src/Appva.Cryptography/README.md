# Appva.Cryptography
### Password Hashing
```javascript
var random = Password.Random();
var hashed = Password.Pbkdf2(original);
Password.Equals(random, hashed);
```

### Hashing
```javascript
Hash.Using<MurmurHash>("foo").Build();
Hash.Random();
Checksum.Using<MurmurHash>("foo").Build();
Checksum.Assert<MurmurHash>("foo").Equals("bar");
```