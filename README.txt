# Receptserver System – Opsætning

**Udarbejdet af:** Jonas Bruun Gregersen  
**Semester:** 4. semester, Datamatikeruddannelsen  

---

## Opsætning

- Projektet bruger **LocalDB**.  
- Databasen oprettes automatisk ved opstart via migrations, inkl. seed-data (3 lægehuse og 3 apoteker).  

### Seedede lægehuse
- Skive Lægehus – Ydernummer: 123456  
- Aarhus Sundhedscenter – Ydernummer: 654321  
- Viborg Lægeklinik – Ydernummer: 112233  

### Seedede apoteker
- Sct. Clement Apotek, Aarhus  
- Skive Apotek, Skive  
- Holstebro Løve Apotek, Holstebro  

---

## Arkitektur

WPF App (lægehus) ↔ API ↔ BLL ↔ DAL ↔ LocalDB  
MVC App (apotek) ↔ BLL ↔ DAL ↔ LocalDB  

- WPF: kommunikerer via API til server/BLL.  
- MVC: kommunikerer direkte med BLL.  
- BLL: forretningslogik, validering, mapper mellem DAL ↔ DTO.  
- DAL: databaseadgang via Entity Framework Core, mapper ligger i DAL, men kunne ligge i BLL for bedre separation of concerns.  
- LocalDB: database oprettes automatisk med migrations og seed-data.

---

## Kørsel

1. **WPF-app (lægehus):** log ind med et ydernummer → opret og håndter recepter.  
2. **MVC-app (apotek):** søg CPR → foretag udleveringer.  

**Alle hovedfunktioner er testet og kører uden fejl.**
