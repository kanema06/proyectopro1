Karen Negrín Mazarío C111
“Maze & Monsters”
	Para jugar:
En mi repositorio hay dos versiones de mi proyecto:
 	“OK” en esta carpeta está “Mazes & Monsters” para jugar desde la consola de VSC sin nada más.
 	“Maze Runner” en esta carpeta está “Mazes & Monsters” para jugar igual desde la consola de VSC pero tiene incorporada música de fondo, para poder utilizarla es necesario el paquete de NAudio, con escribir en la consola una vez abierto el proyecto en VSC, “dotnet add package NAudio” debe funcionar. Prefiero que se juegue esta versión.
	Reglas del juego:

“Mazes & Monsters” es un juego sencillo de laberinto por turnos, cada jugador tendrá un turno para jugar donde puede moverse según el tiro de un dado, utilizar habilidades especiales y terminar el turno en cualquier momento.
El objetivo del juego es llegar a esta casilla: "🏁". El primer jugador en llegar gana.
Para llegar se pueden encontrar con una serie de obstáculos, el efecto de los obstáculos no aparece hasta que no caes en dichos obstáculos para que sea más inesperado, aquí una guía de sus efectos:
"🏘️" las ciudades no causan ningún efecto, son una casilla segura más
 "🏚️" las ruinas son uno de los peores obstáculos del laberinto con -4❤️ de daño  
"⛪" los santuarios son casillas positivas que recuperan 2❤️ al jugador
 "🌼" las praderas también son casillas positivas que recuperan 1❤️ al jugador
 "🟦" los lagos son obstáculos que causan -3❤️ al jugador 
"🧟" los cementerios causan un daño de 1❤️ al jugador 
 "🔴"Este obstáculo representa la presencia de un NPC, como los NPC pueden ser beneficiosos o perjudiciales hasta que no caigas en la casilla no se revela a que NPC te has encontrado.
Los posibles NPC son:
“🐺” un lobo, que causa un daño de 1❤️ al jugador 
 “🧙” una bruja, que juega una ronda de piedra, papel, tijera, lagarto, Spock con le jugador y en dependencia de si gana (quita 1❤️), pierde (recupera1❤️) o empata (no hace nada) realiza el daño.
“🧚” un hada, le recupera 1❤️ de vida al jugador;
El resto de las reglas del juego vienen explicitas durante este.

	Sobre el código:
Mi código está dividido en 4 archivos principales (5 en el caso de la versión con música).  Estos son:
1.	Maze: tiene todo el código de la generación del laberinto, métodos para generar los obstáculos, imprimirlo, etc.
2.	Players: tiene todos los personajes disponibles para jugar, con sus poderes, emojis, descripciones, etc.
3.	Obstacles: tiene todos los obstáculos y NPCs del laberinto, como te afectan, etc.
4.	Program: Aquí hago uso de todas las clases de los archivos anteriores, llamo a sus métodos y utilizo sus objetos, tiene principalmente texto e interacciones con el usuario.
5.	Music: Aquí se ocupa de la música de background del juego, con dos métodos principales, play y stop.

