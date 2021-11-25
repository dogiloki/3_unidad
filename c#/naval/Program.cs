class Program{

    int logitud_tablero=12;
    int barcos=3;
    String[] tablero1;
    String[] tablero2;
    String[] jugadores=new String[2];

    public Program(){
        tablero1=new String[logitud_tablero];
        tablero2=new String[logitud_tablero];
        String volver_jugar;

        do{
            Console.Clear();
            do{
                Console.Write("Nombre del jugador 1: ");
                jugadores[0]=Console.ReadLine();
            }while(jugadores[0]=="");
            Console.Write("\n");
            do{
                Console.Write("Nombre del jugador 2: ");
                jugadores[1]=Console.ReadLine();
            }while(jugadores[1]=="");

            for(int a=0; a<logitud_tablero; a++){
                tablero1[a]=" ";
                tablero2[a]=" ";
            }

            Console.Clear();
            ponerBarcos(barcos,0);
            Console.Write("\n");
            ponerBarcos(barcos,1);
            Console.Clear();
            comenzarJuego();
            Console.Write("\nVolver a jugar s/n: ");
            volver_jugar=Console.ReadLine();
        }while(volver_jugar=="s" || volver_jugar=="S");
    }

    public void comenzarJuego(){
        int turno=0;
        int posicion=1;
        do{
            Console.Clear();
            pintarTablero(0);
            Console.Write("\n");
            pintarTablero(1);
            Console.Write("\nTurno de "+jugadores[turno]+"\nPosición de ataque: ");
            try{
                posicion=Int32.Parse(Console.ReadLine());
            }catch(Exception ex){
                posicion=0;
            }
            if(posicion>logitud_tablero || posicion<=0){
                Console.WriteLine("Posición no válida\nHas fallado en el ataque");
            }else{
                if(turno==0){
                    if(existeBarco(1,posicion)){
                        tablero2[posicion-1]=" ";
                        Console.WriteLine("\nHas eliminado un barco");
                    }else{
                        Console.WriteLine("\nHas fallado el ataque");
                    }
                }else{
                    if(existeBarco(0,posicion)){
                        tablero1[posicion-1]=" ";
                        Console.WriteLine("\nHas eliminado un barco");
                    }else{
                        Console.WriteLine("\nHas fallado el ataque");
                    }
                }
            }
            turno=(turno==1)?0:1;
            Console.WriteLine("\nEnter para continuar...");
            Console.ReadKey();
        }while(!terminoJuego() || posicion>logitud_tablero || posicion<=0);
        Console.Clear();
        pintarTablero(0);
        Console.Write("\n");
        pintarTablero(1);
        Console.WriteLine("\nEl ganador es "+jugadores[ganador()]);
    }

    public int ganador(){
        for(int a=0; a<logitud_tablero; a++){
            if(tablero1[a]!=" "){
                return 0;
            }else
            if(tablero2[a]!=" "){
                return 1;
            }
        }
        return 0;
    }

    public Boolean terminoJuego(){
        Boolean termino=true;
        for(int a=0; a<logitud_tablero; a++){
            if(tablero1[a]!=" "){
                termino=false;
                a=logitud_tablero;
            }
        }
        if(termino){
            return termino;
        }
        termino=true;
        for(int a=0; a<logitud_tablero; a++){
            if(tablero2[a]!=" "){
                termino=false;
                a=logitud_tablero;
            }
        }
        return termino;
    }
    
    public void pintarTablero(int jugador){
        Console.WriteLine("Jugador: "+jugadores[jugador]);
        for(int fila=0; fila<3; fila++){
            for(int columna=1; columna<=logitud_tablero; columna++){
                switch(fila){
                    case 0: Console.Write(" | "+columna+" | "); break;
                    case 1: Console.Write(" | "+espaciosVacios(columna.ToString().Length)+" | "); break;
                    case 2: if(jugador==0){
                                Console.Write(" | "+tablero1[columna-1]+espaciosVacios(columna.ToString().Length-1)+" | ");
                            }else{
                                Console.Write(" | "+tablero2[columna-1]+espaciosVacios(columna.ToString().Length-1)+" | ");
                            }
                            break;
                }
            }
            Console.Write("\n");
        }
    }

    public void ponerBarcos(int barcos, int jugador){
        for(int a=1; a<=barcos; a++){
            Console.Clear();
            pintarTablero(jugador);
            int posicion_barco;
            Boolean existe;
            do{
                Console.Write("Posición de barco "+a+": ");
                try{
                    posicion_barco=Int32.Parse(Console.ReadLine());
                }catch(Exception ex){
                    posicion_barco=0;
                }
                existe=true;
                if(posicion_barco>logitud_tablero || posicion_barco<=0){
                    Console.WriteLine("Posición no válida");
                }else{
                    existe=existeBarco(jugador,posicion_barco);
                    if(existe){
                        Console.WriteLine("Posición ocupada");
                    }
                }
            }while(posicion_barco>logitud_tablero || posicion_barco<=0 || existe);
            if(jugador==0){
                tablero1[posicion_barco-1]="0";
            }else{
                tablero2[posicion_barco-1]="0";
            }
        }
    }

    public Boolean existeBarco(int jugador,int posicion){
        if(jugador==0){
            return (tablero1[posicion-1]!=" ");
        }else{
            return (tablero2[posicion-1]!=" "); 
        }
    }

    public String espaciosVacios(int num){
        String espacios="";
        for(int a=0; a<num; a++){
            espacios+=" ";
        }
        return espacios;
    }

    static void Main(string[] args){
        new Program();
    }
}