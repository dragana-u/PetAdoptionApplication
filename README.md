# PetAdoptionApplication
Оваа веб апликација е наменета за управување со процесот на вдомување животни за курсот интегрирани системи. Корисниците ќе можат да прегледуваат достапни животни за вдомување, да поднесуваат барања за вдомување и да управуваат со податоци за вдомени животни. Апликацијата ќе интегрира надворешно API (Petfinder), преку кое ќе се влечат реални податоци за животни кои се за вдомување, и потоа ќе бидат трансформирани и внесени во нашата база. Ентитети кои би ги имало се: Animal, AdoptionForm, AdoptionShelter, PetAdoptionApplicationUser, Species и DTO за екстерното API: PetFinderAnimal. Релациите меѓу нив би биле: <br>
<ul>
<li>1-* меѓу Species и Animal</li>
<li>1-* меѓу AdoptionShelter и Animal</li>
<li>1-* меѓу Animal и AdoptionForm</li>
<li>1-* меѓу PetAdoptionApplicationUser и AdoptionForm</li>
</ul>

За користење на админ функционалности може да се најавете со: admin@gmail.com Admin123! <br>
За користење функционалности за обичен корисник, доволно е да се регистрирате.

Хостирана на следниот линк: </br>
<a href="https://petadoptionweb20250622175940-ezbke4bbb2f4grf3.canadacentral-01.azurewebsites.net/" target="_blank">https://petadoptionweb20250622175940-ezbke4bbb2f4grf3.canadacentral-01.azurewebsites.net/</a>
