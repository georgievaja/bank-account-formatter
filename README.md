# Standardní řetězce formátu čísla bankovního účtu v ČR

|**Specifikátor formátu**|**Popis**|**Příklady**|
|---|---|---|
|"p"|Prefix bankovního účtu|19-10008890/200 -> 19|
|"P"|Prefix bankovního účtu s doplněním nul zleva do šesti znaků|19-10008890/200 -> 000019|
|"a"|Číslo účtu|19-10008890/0200 -> 10008890|
|"A"|Číslo účtu s doplněním nul zleva do deseti znaků|19-10008890/200 -> 0010008890|
|"b"|Kód banky|19-10008890/0200 -> 200|
|"B"|Kód banky s doplněním nul zleva do čtyřech znaků|19-10008890/200 -> 0200|
|Jakýkoliv jiný znak|Znak je zkopírován do výsledného řetězce beze změny|19-10008890/200 (abc) -> 10008890bc|
