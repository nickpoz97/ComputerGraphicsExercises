# ComputerGraphicsExercises
## Building degli eseguibili (Windows)
1. clonare o scaricare il repo
2. creare una cartella build all' interno del repo scaricato
3. generare la soluzione con cmake con la cartella build come output
4. avviare visual studio tramite la il file .sln
5. compilare tutta la soluzione (consiglio in modalità **release**)
6. gli eseguibili saranno disponibili (insieme alla .dll di assimp) nella cartella bin/*build_type* presente nella root del repo

Su linux la libreria assimp non è stata inserita perchè il peso era maggiore dei 100 mB possibili su github, in ogni caso basta compilarla come shared lib e inserire i file .so in *repo_root*/libs/linux.