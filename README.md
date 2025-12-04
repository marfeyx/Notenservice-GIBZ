# Notenservice-GIBZ
Schulprojekt Notenservice GIBZ

/// GRUPPENMITGLIEDER ///

- Jakob Trost
- Livio Rütter
- Jan Frischknecht

/// DOKUMENTATION ///
Wir begannen indem wir uns überlegten was für eine Applikation wir machen könnten,
um das Problem der Prorektoren und der Lehrer zu lösen.
Dafür bekamen wir eine Vorlage zu einer Nutzwertanalyse, worauf bei uns Blazor am besten abschnitt.
Da wir letztlich ein Ük modul zu blazor hatten, entschieden wir uns schliesslich dafür,
und erstellten als Gerüst diese Solution mit Visual Studio 2022, welche wir dann auf github pushten.
Es ist wichtig zu beachten, dass wir diese solution aus teilen von unseren alten Solustions kopierten,
welche wir im Ük erstellten.


/// ERKLÄRUNG DER FEATURES ///


/// ANFORDERUNGEN DES PROREKTORS ///

KI als Rundungshilfe
	KI zu implementieren liegt über unseren Fähigkeiten, aber wir haben uns einige Möglichkeiten überlegt, das Rundunssystem fair und hilfreich zu gestalten.
	Unsere ersten Ideen beinhalten:
		Mathematisch Runden -> Nachteile: Teilweise unfair, wenn z.B. immer abgerundet wird.
		Nach gefühl runden -> Nachteile: Subjektiv und eventuell unfair.
		"KI-hilfe" -> Anfangs wird mathematisch gerundet, aber wenn oft abgerundet wird (z.B. immer wenn der Schüler mehr als 0.5 Note durch Runden verliert), wird der Lehrer benachrichtigt und kann manuell eingreifen.

Blockchain-Verschlüsselung
	Eine Blockchain-Verschlüsselung zu implementieren ist sehr komplex und liegt ausserhalb unseres Könnens. 
	Ebenfalls ist eine Blockchain-Verschlüsselung übertrieben und unpassend.
	Dennoch haben wir uns Gedanken gemacht, wie wir die Sicherheit der Benutzerdaten gewährleisten können.
		Wir implementieren ein sicheres Login-System mit verschlüsselten Passwörtern, damit alle Daten geschützt sind.

Sicherer Login
	Einen sicheren Login zu implementieren ist machbar und liegt in unserem Fähigkeitsbereich.
	Wir planen, ein Login-System zu erstellen, das folgende Sicherheitsmerkmale beinhaltet:
		- Verschlüsselte Passwörter: Passwörter werden mit einem starken Hashing-Algorithmus gespeichert.
		- Sichere Sitzungen: Sitzungen werden sicher verwaltet, und laufen nach 30 Minuten ab.