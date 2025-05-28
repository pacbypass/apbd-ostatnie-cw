Po odpaleniu w riderze, wystawia http na `http://localhost:1337`

Na potrzebe zrobilem kilka testów w formie skryptów curl w `ostatniezadanie_s27359/test-scripts`

### Individual Tests

Run each test script individually by double-clicking on the `.bat` files:

Jeśli chce się Panu sprawdzać kody zwrotu z curl to trzeba dodać -v do komend curlowych
1. **1-create-prescription-new-patient.bat** - Tworzy recepte dla nowego pacjenta
2. **2-create-prescription-existing-patient.bat** - Tworzy recepte dla istniejącego
3. **3-get-patient-details.bat** - Pobierz dane o pacjencie i jego receptach
4. **4-invalid-duedate.bat** - (DueDate < Date) - powinno zwrocic 400
5. **5-nonexistent-medicament.bat** - Nieistniejący lek - powinno zwrocic 400
6. **6-too-many-medicaments.bat** - za duzo lekow na recepcie - powinno zwrocic 400
7. **7-nonexistent-patient.bat** - Pobiera nieistniejacego pacjenta - powinno byc ret 404

## Endpointy

- POST /api/prescriptions - tworzy nową receptę
- GET /api/patients/{id} - pobiera szczegóły pacjenta z receptami

