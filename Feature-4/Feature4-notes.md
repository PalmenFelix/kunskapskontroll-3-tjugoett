# 4. Satsningar och marker // Snabbdokumentation

## Funktioner 
Varje spelare har ett antal marker och satsar innan given
Vinst betalar 1:1 — insatsen tillbaka plus lika mycket till
Naturlig tjugoett betalar 3:2
Lika ger insatsen tillbaka
En spelare som får slut på marker lämnar bordet
Insatsen får inte vara större än spelarens marker, och inte noll

## Dokumentation
1. Skapa chips klass
2. Funktioner i Chips, AddChips, RemoveChips
3. New chips i Player.cs från GetChips
4. Placebets i Round.cs
5. ShowChips i ConsoleUI
6. AskBet i ConsoleUI
7. ShowChips och AskBet i Round.cs
8. Dicitionry av players och bets tillagd i Round
9. If-sats tillagd i PlaceBets i Round
10. Payout resultat tillagd i Round.cs 
11. Payout metod fixad i Round.cs
12. Nu tas spelare utan marker bort och det meddelar när alla spelare har slut, ligger tidigt i Round.cs och PlayerRemoved meddelande i ConsoleUI.
13. Game.cs redigerat, så att round inte kan köras om det inte finns några tillgängliga spelare.
14. Lade till SaveChips() i Chips.cs och stöd för spelarens marker i PlayerData och GameStorage.cs.
15. Uppdaterade sparning och laddning så att spelarens marker ingår i spelaruppgifterna.
16. Uppdaterade SetUpPlayers() så att en befintlig spelare kan väljas igen utan att en dubblett skapas. 
17. (DESIGNVAL) En spelare som får slut på marker lämnar bordet och kan inte vara med i fler rundor under samma spelsession. Om en spelare som har förlorat alla sina marker får 1000 marker igen om en ny spelsession körs. Spelare som fortfarande har marker kvar behåller sitt sparade saldo mellan olika spelsessions och programkörningar.
18. Kommentarer tillagda där jag lagt till kod eller redigerat kod



