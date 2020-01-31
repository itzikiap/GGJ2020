using System.Collections.Generic;

public class DBConnector
{

    DataService ds;
  
    public DBConnector()
    {
       ds = new DataService("ggj2020.db");
    }

    public Save GetLastSave()
    {
        return ds.GetUltimoSave();
    }
    public void Salvar(int nivel, int puntos, string name)
    {
        ds.CreateSave(name, nivel, puntos);
    }

    public   List<Pregunta> GetPreguntasNivel1()
    {
        return ds.GetPreguntasbyNivel(1);
    }
    public   List<Pregunta> GetPreguntasNivel2()
    {
        return ds.GetPreguntasbyNivel(2);
    }
    public   List<Pregunta> GetPreguntasNivel3()
    {
        return ds.GetPreguntasbyNivel(3);
    }
    public   List<Pregunta> GetPreguntasNivel4()
    {
        return ds.GetPreguntasbyNivel(4);
    }
    public   List<Pregunta> GetPreguntasNivel5()
    {
        return ds.GetPreguntasbyNivel(5);
    }
  
    public int getCantPreguntasNivel1()
    {
        return ds.getCantRefPreguntasNivel(1);
    }
    public int getCantPreguntasNivel2()
    {
        return ds.getCantRefPreguntasNivel(2);
    }
    public int getCantPreguntasNivel3()
    {
        return ds.getCantRefPreguntasNivel(3);
    }
    public int getCantPreguntasNivel4()
    {
        return ds.getCantRefPreguntasNivel(4);
    }
    public int getCantPreguntasNivel5()
    {
        return ds.getCantRefPreguntasNivel(5);
    }


}

