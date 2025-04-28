﻿using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel;
using System;
using System.Linq;

public class JsonHelper
{
    public static List<NotificacionJson> CargarNotificacionesJson(string archivoJson)
    {
        List<NotificacionJson> notificaciones = new List<NotificacionJson>();

        if (File.Exists(archivoJson))
        {
            string json = File.ReadAllText(archivoJson);
            notificaciones = JsonConvert.DeserializeObject<List<NotificacionJson>>(json);
        }
        else
        {
            File.WriteAllText(archivoJson, "[]");
        }

        return notificaciones;
    }

    public static List<NotificacionJson> CargarNotificacionesJsonFiltro(string archivoJson, string x)
    {
        List<NotificacionJson> notificaciones = new List<NotificacionJson>();

        if (File.Exists(archivoJson))
        {
            string json = File.ReadAllText(archivoJson);
            notificaciones = JsonConvert.DeserializeObject<List<NotificacionJson>>(json);
        }
        else
        {
            File.WriteAllText(archivoJson, "[]");
        }

        string[] ids = x.Split(',').Select(id => id.Trim()).ToArray();

        List<NotificacionJson> notificacionesFiltradas = notificaciones
            .Where(n => ids.Contains(n.ID.ToString()))
            .ToList();

        List<NotificacionJson> notificacionesConCampos = notificacionesFiltradas.Select(n =>
        {
            var notiFiltrada = new NotificacionJson();

            notiFiltrada.TIPO = n.TIPO;
            notiFiltrada.DESCRIPCIÓN = n.DESCRIPCIÓN;
            notiFiltrada.REMITENTE = n.REMITENTE;
            notiFiltrada.FECHA = n.FECHA;

            return notiFiltrada;
        }).ToList();

        notificacionesConCampos.OrderByDescending(n => n.FECHA).ToList();

        return notificacionesConCampos;
    }

    // notificacion
    public static void GuardarNotificacionesJson(List<NotificacionJson> notificaciones, string archivoJson)
    {
        string nuevoJson = Newtonsoft.Json.JsonConvert.SerializeObject(notificaciones, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(archivoJson, nuevoJson);
    }

    public static BindingList<NotificacionJson> ConvertirAListaDeBindingList(List<NotificacionJson> listaNotificaciones)
    {
        return new BindingList<NotificacionJson>(listaNotificaciones);
    }

    // seccion
    public static bool GuardarSeccionJson(SeccionJson nuevaSeccion, string archivoJson)
    {
        try
        {
            List<SeccionJson> secciones = new List<SeccionJson>();

            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                secciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SeccionJson>>(jsonExistente) ?? new List<SeccionJson>();
            }

            secciones.Add(nuevaSeccion);

            string nuevoJson = Newtonsoft.Json.JsonConvert.SerializeObject(secciones, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(archivoJson, nuevoJson);
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public static bool actualizarSeccionJson(SeccionJson viejaSeccion, SeccionJson nuevaSeccion, string archivoJson)
    {
        try
        {
            List<SeccionJson> secciones = new List<SeccionJson>();

            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                secciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SeccionJson>>(jsonExistente) ?? new List<SeccionJson>();
            }

            int indice = secciones.FindIndex(s =>
                s.NOMBRE == viejaSeccion.NOMBRE &&
                s.DESCRIPCIÓN == viejaSeccion.DESCRIPCIÓN &&
                s.FILTRO == viejaSeccion.FILTRO
            );

            if (indice != -1)
            {
                secciones[indice] = nuevaSeccion;

                string nuevoJson = Newtonsoft.Json.JsonConvert.SerializeObject(secciones, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(archivoJson, nuevoJson);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool eliminarSeccionJson(SeccionJson Seccion, string archivoJson)
    {
        try
        {
            List<SeccionJson> secciones = new List<SeccionJson>();

            if (File.Exists(archivoJson))
            {
                string jsonExistente = File.ReadAllText(archivoJson);
                secciones = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SeccionJson>>(jsonExistente) ?? new List<SeccionJson>();
            }

            int indice = secciones.FindIndex(s =>
                s.NOMBRE == Seccion.NOMBRE &&
                s.DESCRIPCIÓN == Seccion.DESCRIPCIÓN &&
                s.FILTRO == Seccion.FILTRO
            );

            if (indice != -1)
            {
                secciones.RemoveAt(indice); 

                string nuevoJson = Newtonsoft.Json.JsonConvert.SerializeObject(secciones, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(archivoJson, nuevoJson);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }


    public static List<SeccionJson> cargarSeccionJson(string archivoJson)
    {
        List<SeccionJson> secciones = new List<SeccionJson>();

        if (File.Exists(archivoJson))
        {
            string json = File.ReadAllText(archivoJson);
            secciones = JsonConvert.DeserializeObject<List<SeccionJson>>(json);
        }
        else
        {
            File.WriteAllText(archivoJson, "[]");
        }

        return secciones;
    }
}
