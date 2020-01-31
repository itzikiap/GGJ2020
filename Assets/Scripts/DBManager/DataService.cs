using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService
{

    private SQLiteConnection _connection;

    public DataService(string DatabaseName)
    {

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
            var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                                                                                     // then save to Application.persistentDataPath
            File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
 
        Debug.Log("Final PATH: " + dbPath);
             
    }

    public IEnumerable<Pregunta> Refpreguntas { get; set; }
    public IEnumerable<Save> RefSave { get; set; }


    public List<PORDTXT> preguntasORDTXT_1 { get; set; }
    public List<PARRTXTIMG> preguntasARRTXTIMG_2 { get; set; }
    public List<PSEL> preguntasSEL_3 { get; set; }
    public List<PATT> preguntasATT_4 { get; set; }
    public List<PComplet> preguntasCompletar_5 { get; set; }


    public void inicializarPreguntasSalvas()
    {
        preguntasORDTXT_1 = GetPreguntasORDTXT();
        preguntasARRTXTIMG_2 = GetPreguntasARRTXTIMG();
        preguntasSEL_3 = GetPreguntasSEL();
        preguntasATT_4 = GetPreguntasATT();
        preguntasCompletar_5 = GetPreguntasCompletar();
        Refpreguntas = GetRefPreguntas();
        RefSave = GetSalvas();
    }
    
    public IEnumerable<CasosEstudio> GetCasosEstudio()
    {
        return _connection.Table<CasosEstudio>();
    }
    public IEnumerable<Imagen> GetImagenes()
    {
        return _connection.Table<Imagen>();
    }
    public IEnumerable<Sentence> GetNiveles()
    {

        return _connection.Table<Sentence>();

       
    }

 
    private IEnumerable<Pregunta> GetRefPreguntas()
    {
        return _connection.Table<Pregunta>();
    }
   public List<Pregunta> GetPreguntasbyNivel(int nivel) {
       List<Pregunta> preguntas = new List<Pregunta>();
       foreach (Pregunta p in Refpreguntas)
       {
            if(p.nivel == nivel)
            {
                preguntas.Add(encontrarPreg(p));
            }
       }

        return preguntas;
    } 

    private Pregunta encontrarPreg(Pregunta p)
    {
        switch (p.tipo)
        {
            case 1:
                foreach (PORDTXT p1 in preguntasORDTXT_1) {
                    if (p1.id == p.id) return p1;
                }
            break;
            case 2:
                foreach (PARRTXTIMG p1 in preguntasARRTXTIMG_2)
                {
                    if (p1.id == p.id) return p1;
                }
                break;
            case 3:
                foreach (PSEL p1 in preguntasSEL_3)
                {
                    if (p1.id == p.id) return p1;
                }
                break;
            case 4:
                foreach (PATT p1 in preguntasATT_4)
                {
                    if (p1.id == p.id) return p1;
                }
                break;
            case 5:
                foreach (PComplet p1 in preguntasCompletar_5)
                {
                    if (p1.id == p.id) return p1;
                }
                break;
        }
        return null;
    }
    
    public IEnumerable<Texto> GetTextos()
    {
        return _connection.Table<Texto>();
    }

    //Pregunta tipo 1 o de ordenar texto dado su id
    private List<PORDTXT> GetPreguntasORDTXT()
    {
        IEnumerable<Pregunta> ps = _connection.Table<Pregunta>().Where(x => x.tipo == 1);
        List<PORDTXT> parrtxts = new List<PORDTXT>();
        foreach (Pregunta p1 in ps)
        {
            IEnumerable<P1> prr = _connection.Table<P1>().Where(x => x.id_preg == p1.id);//obtiene las preguntas que coincidan

            PORDTXT temp = new PORDTXT();
            temp.id = p1.id;
            temp.nivel = p1.nivel;
            temp.tipo = p1.tipo;
            temp.enunciado = p1.enunciado;

            List<KeyValuePair<Texto, int>> textOrden  = new List<KeyValuePair<Texto, int>>();
            foreach (P1 p2 in prr)
            {
                KeyValuePair<Texto, int> pair = new KeyValuePair<Texto, int>(GetTexto(p2.id_texto), p2.posicion);
                textOrden.Add(pair);
            }
            parrtxts.Add(temp);
            temp.textOrden = textOrden;
        }
        return parrtxts;
    }

    //Obtener las preguntas de tipo 2 o de arrastrar texto a imagen
    private List<PARRTXTIMG> GetPreguntasARRTXTIMG()
    {
        IEnumerable<Pregunta> ps = _connection.Table<Pregunta>().Where(x => x.tipo == 2);
        List<PARRTXTIMG> parrtxts = new List<PARRTXTIMG>();
        foreach (Pregunta p1 in ps) {
            IEnumerable<P2> prr = _connection.Table<P2>().Where(x => x.id_preg == p1.id);//obtiene las preguntas que coincidan

            PARRTXTIMG temp = new PARRTXTIMG();
            temp.id = p1.id;
            temp.nivel = p1.nivel;
            temp.tipo = p1.tipo;
            temp.enunciado = p1.enunciado;

            List<KeyValuePair<Texto, Imagen>> txtsAImgs = new List<KeyValuePair<Texto, Imagen>>();
            foreach(P2 p2 in prr){
                KeyValuePair<Texto, Imagen> pair = new KeyValuePair<Texto, Imagen>(GetTexto(p2.id_txt),GetImagen(p2.id_img));
                txtsAImgs.Add(pair);
            }
            temp.txtsAImgs = txtsAImgs;
            parrtxts.Add(temp);
        }
        return parrtxts;
    }

    //Pregunta tipo 3 o de seleccionar
    private List<PSEL> GetPreguntasSEL()
    {
        IEnumerable<Pregunta> ps = _connection.Table<Pregunta>().Where(x => x.tipo == 3); //obtiene todas las preguntas de tipo 3
        List<PSEL> parrtxts = new List<PSEL>();
        foreach (Pregunta p1 in ps)
        {
            IEnumerable<P3> prr = _connection.Table<P3>().Where(x => x.id_preg == p1.id);//obtiene las preguntas que coincidan

            PSEL temp = new PSEL();
            temp.id = p1.id;
            temp.nivel = p1.nivel;
            temp.tipo = p1.tipo;
            temp.enunciado = p1.enunciado;

            List<KeyValuePair<Texto, bool>> textOrden = new List<KeyValuePair<Texto, bool>>();
            foreach (P3 p2 in prr)
            {
                KeyValuePair<Texto, bool> pair = new KeyValuePair<Texto, bool>(GetTexto(p2.id_txt), p2.es_valido);
                textOrden.Add(pair);
            }
            temp.seleccion = textOrden;
            parrtxts.Add(temp);
        }
        return parrtxts;
    }

    //Obtener las preguntas de tipo 4 o de arrastrar texto a texto
    private List<PATT> GetPreguntasATT()
    {
        IEnumerable<Pregunta> ps = _connection.Table<Pregunta>().Where(x => x.tipo == 4);

        List<PATT> parrtxts = new List<PATT>();
        foreach (Pregunta p1 in ps)
        {
            IEnumerable<P4> prr = _connection.Table<P4>().Where(x => x.id_preg == p1.id);//obtiene las preguntas que coincidan

            PATT temp = new PATT();
            temp.id = p1.id;
            temp.nivel = p1.nivel;
            temp.tipo = p1.tipo;
            temp.enunciado = p1.enunciado;

            List<KeyValuePair<Texto, Texto>> txtsAtxts = new List<KeyValuePair<Texto, Texto>>();
            foreach (P4 p2 in prr)
            {
                KeyValuePair<Texto, Texto> pair = new KeyValuePair<Texto, Texto>(GetTexto(p2.id_txt1), GetTexto(p2.id_txt2));
                txtsAtxts.Add(pair);
            }
            temp.textoAtexto = txtsAtxts;
            parrtxts.Add(temp);
        }
        return parrtxts;
    }

    //Obtener las preguntas de tipo 5 o de completar 
    public List<PComplet> GetPreguntasCompletar()
    {
        IEnumerable<Pregunta> ps = _connection.Table<Pregunta>().Where(x => x.tipo == 5);
        List<PComplet> parrtxts = new List<PComplet>();
        foreach (Pregunta p1 in ps)
        {
            IEnumerable<P5> prr = _connection.Table<P5>().Where(x => x.id_preg == p1.id);//obtiene las preguntas que coincidan

            PComplet temp = new PComplet();
            temp.id = p1.id;
            temp.nivel = p1.nivel;
            temp.tipo = p1.tipo;
            temp.enunciado = p1.enunciado;

            List<Texto> textos = new List<Texto>();
            foreach(P5 p2 in prr)
            {
                textos.Add(GetTexto(p2.id_text));
            }

            temp.text = textos;
            parrtxts.Add(temp);
        }
        return parrtxts;
    }

    public Imagen GetImagen(int id)
    {
        IEnumerable<Imagen> imagenes = _connection.Table<Imagen>().Where(x => x.id == id);
        foreach (var i in imagenes)
        {
           return i;
        }
        return null;
    }

    public Texto GetTexto(int id)
    {
        IEnumerable<Texto> textos = _connection.Table<Texto>().Where(x => x.id == id);
        foreach(var t in textos) {
            return t;
        }
        return null;
    }

    public int getCantRefPreguntasNivel(int nivel)
    {
        return _connection.Table<Pregunta>().Where(x => x.nivel == nivel).Count();
    }
        
    private IEnumerable<Save> GetSalvas()
    {
        return _connection.Table<Save>();
    }
   
    public Save GetUltimoSave()
    {
        bool firstIteration = false;
        Save auxSave = null;
        foreach (Save s in RefSave)
        {
            if (!firstIteration) {
                auxSave = s;
                firstIteration = true;
            }
            System.DateTime d0 = System.DateTime.Parse(auxSave.fecha);
            System.DateTime d1 = System.DateTime.Parse(s.fecha);

                                 
            if(System.DateTime.Compare(d0, d1) < 0)
            {
                //cuando la fecha de d1 es mas nueva que la de d2
                auxSave = s;
            }
        }
        return auxSave;
    }

    public Save CreateSave(string name, int nivel, int puntos)
    {
        
        Save s = new Save();
        s.name = name;
        s.nivel = nivel;
        s.puntos = puntos;
        s.fecha = System.DateTime.Now.ToLongDateString();
        Debug.Log("Creado nueva salva para " + s);
        _connection.Insert(s);
        return s;
    }

}
