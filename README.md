# Designbeslut

### Ska card ha value?
Ja, card har value 2-9 har sig själv som value, 10-kung har 10 som value och ess har 1 eller 11 beroende på vad som inte spräcker.  

### Är dealer en spelare?
Nej, dealern ska inte räknas som spelare.  

### Vem bestämmer vad en spelare gör?
Från början: ConsoleUI.

Ändrat i tillval 2: nu bestämmer spelarens strategi (IPlayerStrategy). En människa har HumanStrategy, som fortfarande frågar via ConsoleUI (H eller S). En datorspelare bestämmer själv. Vi ändrade det för att datorspelare också ska kunna sitta vid bordet.

### Var ligger givarens regler?
Regler för givaren ska ligga under dealer.



### Hur du startar programet

Du startar genom att köra dotnet run 
```C#
public class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}
```
### Översikt över klasserna
----

1. Programmet är uppdelat i flera klasser som tillsammans hanterar spelets logik, korten och användargränssnittet.
2. Card: Representerar ett spelkort med en valör och en färg.
3. Deck / Shoe: Hanterar kortleken och de kort som används under spelets gång.
4. Hand: Innehåller en deltagares kort och beräknar handens totala värde.
5. Player: Representerar en spelare som deltar i spelet.
6. Dealer: Representerar dealern och hanterar dealerns roll i spelet.
7. Round: Samordnar de olika momenten i en spelrunda och kopplar samman spelets olika delar.
8. CardRenderer: Skapar den grafiska representationen av spelkort i konsolen.
9. ConsoleUI: Hanterar kommunikationen med användaren och visar information i konsolen.
10. IPlayerStrategy: Interface som bestämmer spelarens drag (Hit eller Stand).
11. SimpleStrategy, ProStrategy, HumanStrategy: Olika strategier för datorspelare och människor.

Klasserna har olika ansvarsområden men samarbetar för att spelet ska fungera. 


__Peter tillval 1__
kortgrafik i konsolen: 
Gör så att korten reprensenteras i konsolen 
med ram, suit, rank.

__Daria tillval 2__
Datorspelare med strategi:
Man kan välja människa eller dator för varje plats. Alla strategier har samma interface, IPlayerStrategy.
SimpleStrategy tar kort under 17, ProStrategy följer grundstrategin i en tabell, HumanStrategy frågar spelaren H eller S.
Tabellen har bara Hit och Stand, eftersom Double och Split inte finns i spelet.

Philip tillval: 3
Husregler från konfigurationsfil:
skapar justerbara regler så man kan spela spelet på olika sätt,
som om det vore olika kasinon.

Felix tillval: 4
Detta tillvalet handlade om att lägga till en ekonomi. Alla spelare blir tilldelade 1000 marker när de börjar spela. 
När varje runda börjar satsar man ett antal marker, sen hanteras utbetalningar vid vanlig vinst, naturlig blackjack och oavgjort.
Förlust resulterar i att man förlorar sina satsade marker. Spelarnas saldo sparas mellan programkörningar, och spelare som får slut på marker lämnar bordet. 
En spelare med 0 marker kan börja om med 1000 marker i en ny spelsession.

Hamed tillval: 5


Vi har inte avvikigt från uppgiftens och spelets grundkrav.


### Kända brister
----

* Datorspelarnas marker sparas inte mellan spelsessioner.
  Från tillval 2 har det skapads datorstyrda spelare som inte får sitt antal marker sparade mellan spelsessioner.
  Utan det är bara mänskliga spelares saldo som sparas.
* Antalet kortlekar följer inte husreglerna från tillval 3.
  Spelet använder ett fast antal kortlekar istället för att följa NumberOfDecks som finns i houserules.txt.


### __Klassdiagram__


![alt text](image.png)


