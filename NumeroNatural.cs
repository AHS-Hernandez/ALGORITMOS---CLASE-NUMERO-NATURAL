using System;

public class Program
{
    public static void Main()
    {
        NumeroNatural num = new NumeroNatural(125);
        
        Console.WriteLine($"Numero original: {num.obtenerValor()}");
        Console.WriteLine($"Cantidad de digitos: {num.CantDigitos()}");
        
        num.InsertarDigito(9, 2); 
        Console.WriteLine($"Despues de insertar 9 en pos 2: {num.obtenerValor()}");
        
        num.ponerNumero(78291);
        Console.WriteLine($"\nNuevo numero: {num.obtenerValor()}");
        
        num.OrdenarDigitosBurbuja();
        Console.WriteLine($"Ordenado (Burbuja): {num.obtenerValor()}");

        num.ponerNumero(78291);
        num.OrdenarDigitosBusqueda();
        Console.WriteLine($"Ordenado (busqueda): {num.obtenerValor()}");
        
        num.ponerNumero(125);
        Console.WriteLine($"\nEn literal: {num.ToLiteral()}");
    }
}

public class NumeroNatural
{
    private int numero;
    public NumeroNatural()
    {
        numero = 1;
    }

    public NumeroNatural(int valor)
    {
        if (valor < 0) valor *= -1;
        this.numero = valor;
    }
    public void ponerNumero(int num)
    {
        if (num >= 0) this.numero = num;
    }
    public int obtenerValor(){ return this.numero;}
    
    //1.Cantidad de dígitos
    public int CantDigitos()
    {
        if (numero == 0) return 1;
        return (int)Math.Log10(numero) + 1;
    }
    
    //2.Insertar un dígito X en la posición Y
    public void InsertarDigito(int digito, int posicion)
    {
        if (digito < 0 || digito > 9) return;
        int cantDig = CantDigitos();
        
        if (posicion < 1) posicion = 1;
        if (posicion > cantDig + 1) posicion = cantDig + 1;
        int div = (int)Math.Pow(10, (cantDig - posicion + 1));
        this.numero = ((numero / div) * 10 + digito) * div + numero % div;
    }
    
   //3.Ordenar dígitos (Bouble sort)
    public void OrdenarDigitosBurbuja()
    {
        int n = CantDigitos();
        
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                int divDer = (int)Math.Pow(10, j);
                int divIzq = (int)Math.Pow(10, j + 1);
                int digitoDer = (numero / divDer) % 10;
                int digitoIzq = (numero / divIzq) % 10;

                //Si el de la izquierda >, desordenados y switch
                if (digitoIzq > digitoDer)
                {
                    numero = numero - (digitoIzq * divIzq) - (digitoDer * divDer)
                                    + (digitoDer * divIzq) + (digitoIzq * divDer);
                }
            }
        }
    }

    //3.Ordenar dígitos (bssqueda matematica)
    public void OrdenarDigitosBusqueda()
    {
        int nuevoNumero = 0;
        for (int dig = 1; dig <= 9; dig++)
        {
            int temp = numero;
            while (temp > 0)
            {
                int digitoActual = temp % 10;
                if (digitoActual == dig)
                {
                    nuevoNumero = nuevoNumero * 10 + dig;
                }
                temp /= 10;
            }
        }
        this.numero = nuevoNumero;
    }

    //4.Convertir a Literal
    public string ToLiteral()
    {
        if (numero == 0) return "cero";
        string literal = "";

        int mil = (numero % 1000000) / 1000;
        int cent = numero % 1000;

        if (mil > 0)
        {
            if (mil == 1) literal += "mil ";
            else literal += DeA3(mil) + " mil ";
        }

        if (cent > 0) literal += DeA3(cent);

        return literal.Trim();
    }

    private string DeA3(int num)
    {
        string literal = "";
        string[,] cad = new string[4, 10]
        {
            {"", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve"},
            {"diez", "once", "doce", "trece", "catorce", "quince", "dieciseis", "diecisiete", "dieciocho", "diecinueve"},
            {"", "", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"},
            {"", "cien", "doscientos ", "trescientos ", "cuatrocientos ", "quinientos ", "seiscientos ", "setecientos ", "ochocientos ", "novecientos "}
        };

        int c = num / 100;
        int d = (num / 10) % 10;
        int u = num % 10;

        if (c == 1)
        {
            if (d == 0 && u == 0) return cad[3, 1];
            else literal += "ciento ";
        }
        else literal += cad[3, c];

        if (d == 1)
        {
            literal += cad[1, u];
            return literal;
        }
        else if (d == 2)
        {
            if (u != 0) return literal + "veinti" + cad[0, u];
            else return literal + cad[2, d];
        }
        else if (d > 2)
        {
            literal += cad[2, d];
            if (u != 0) literal += " y ";
        }

        literal += cad[0, u];
        return literal;
    }
}
