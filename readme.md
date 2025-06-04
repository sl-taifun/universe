# Structure des tables pour la démo ADO & EFCore

## Planet
```
- Id        INT
- Name         NVARCHAR
- Satelite  INT
- Gravity   DECIMAL
```
## Galaxy
```
- Id        INT
- Name      NVARCHAR
```
## Star
```
- Id        INT
- Name      NVARCHAR
- isDeath   BOOLEAN
```
# Relation entre les tables
One to many :
- Planet & Galaxy
Many to many :
-  Planet & Star