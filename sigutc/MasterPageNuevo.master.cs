//using ClassLibraryMatriculas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageNuevo : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // PERSONAL p = new PERSONAL();

        //var lisp = p.LoadPERSONAL("xCedula", Context.User.Identity.Name, "", "", "");
        //if (lisp.Count > 0)
        //{
        //    lblNombres.InnerText = lisp[0].nombre_alu.ToString();
        //    lblCedula.InnerText = lisp[0].cedula_alu.ToString();
        //    imgFoto.ImageUrl = lisp[0].foto_alu;
        //}

        string contenido = "";
        
        contenido += @"<li class='active'>
                                  <a href = '/sigutc/academic/private/default' class='waves-effect waves-dark' style='background: #337ab7 !important;'>
                                      <span class='pcoded-micon'><i class='ti-home'></i><b>D</b></span>
                                      <span class='pcoded-mtext' data-i18n='nav.dash.main'>Home</span>
                                      <span class='pcoded-mcaret'></span>
                                  </a>
                              </li>";

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "ALUMNO") ||
        //    IsSeretarioAcademico("isSecretaryAcademicFacultad", Context.User.Identity.Name, "", "", ""))
        //{
        //    var list_menu_alumno = ListMenu_Alumno();
        //    foreach (var item in list_menu_alumno)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "DOCENTE") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "EVAL") ||
        //    IsSeretarioAcademico("isSecretaryAcademicFacultad", Context.User.Identity.Name, "", "", ""))
        //{
        //    var list_menu_docente = ListMenu_Docente();
        //    foreach (var item in list_menu_docente)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //if (IsSeretarioAcademico("isSecretaryAcademicFacultad", Context.User.Identity.Name, "", "", ""))
        //{
        //    var list_menu_academico = ListMenu_Academico();
        //    foreach (var item in list_menu_academico)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "EVAL") ||
        //    IsSeretarioAcademico("isSecretaryAcademicFacultad", Context.User.Identity.Name, "", "", ""))
        //{
        //    var list_menu_evaluacion = ListMenu_Evaluacion();
        //    foreach (var item in list_menu_evaluacion)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "DOCENTE") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "ALUMNO") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "EVAL") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "DASH_AUTORIDADES") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "DASHBOARD") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "BIENESTAR") ||
        //    IsSeretarioAcademico("isSecretaryAcademicFacultad", Context.User.Identity.Name, "", "", ""))
        //{
        //    var list_menu_publico = ListMenu_Publico();
        //    foreach (var item in list_menu_publico)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //if (IsSeretarioAcademico("isSecretaryAcademicFacultad", Context.User.Identity.Name, "", "", ""))
        //{
        //    var list_menu_titulacion = ListMenu_Titulacion();
        //    foreach (var item in list_menu_titulacion)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //ROLES ELIMINADOS 
        //Roles.IsUserInRole(Context.User.Identity.Name, "DOCENTE")

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "TESO1_DEL") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "TESO1") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "DASHBOARD") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "BIENESTAR"))
        //{
        //    var list_menu_tesoreria = ListMenu_Tesoreria();
        //    foreach (var item in list_menu_tesoreria)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //////////**activar el siguiente código para menu Gestión Académica
        //var list_menu_gestion_academica = ListMenu_Gestion_Academica();
        //foreach (var item in list_menu_gestion_academica)
        //{
        //    contenido = string.Concat(contenido, Menu_CD(item));
        //}

        //////////**activar el siguiente código para menu calendario
        //var list_menu_calendario = ListMenu_Calendario();
        //foreach (var item in list_menu_calendario)
        //{
        //    contenido = string.Concat(contenido, Menu_CD(item));
        //}

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "SFC") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "PER_CAL_SFC"))
        //{
        //    var list_menu_gestion_academica = ListMenu_Gestion_Academica();
        //    foreach (var item in list_menu_gestion_academica)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        //if (Roles.IsUserInRole(Context.User.Identity.Name, "PERIODOG_CALENDAR") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "PERIODOAC_CALENDAR") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_MATRICULAS") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_CALIF") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_SS") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_IDIOMAS") ||
        //    Roles.IsUserInRole(Context.User.Identity.Name, "PER_CAL_SFC"))
        //{
        //    var list_menu_calendario = ListMenu_Calendario();
        //    foreach (var item in list_menu_calendario)
        //    {
        //        contenido = string.Concat(contenido, Menu_CD(item));
        //    }
        //}

        ulListMenu.InnerHtml = contenido;
    }

    private List<Menu_Master> ListMenu_Alumno()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU ALUMNO
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Alumno";
        item1.Usuario = "alumno";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();
        
        ///->DATOS DEL ALUMNO
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Datos del alumno";
        sub_item2.Usuario = "alumno";
        sub_item2.Url = "/sigutc/academic/private/updatealumno.aspx";
        sub_item2.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item2);
        ///->END DATOS DEL ALUMNO

        ///->INSCRIPCIÓN CENTROS (IDIOMAS, CCFF)
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "3";
        sub_item3.Nombre = "Inscripción Centros(IDIOMAS, CCFF)";
        sub_item3.Usuario = "alumno";
        sub_item3.Url = "/sigutc/academic/private/inscripcionCentros.aspx";
        sub_item3.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item3);
        ///->END INSCRIPCIÓN CENTROS (IDIOMAS, CCFF)

        ///->MATRICULAS
        Menu_Master sub_item4 = new Menu_Master();
        sub_item4.Id = "4";
        sub_item4.Nombre = "Matriculas";
        sub_item4.Usuario = "alumno";
        sub_item4.Url = "/sigutc/academic/private/matriculaOnLine.aspx";
        sub_item4.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item4);
        ///->END MATRICULAS

        ///->DESEMPEÑO DOCENTE
        Menu_Master sub_item5 = new Menu_Master();
        sub_item5.Id = "5";
        sub_item5.Nombre = "Desempeño docente";
        sub_item5.Usuario = "alumno";
        sub_item5.Url = "/sigutc/academic/eval/cuestionarioEval.aspx";
        sub_item5.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item5);
        ///->END DESEMPEÑO DOCENTE
        /// 

        ///->BIENESTAR E..
        Menu_Master sub_item6 = new Menu_Master();
        sub_item6.Id = "1";
        sub_item6.Nombre = "Bienestar E...";
        sub_item6.Usuario = "alumno";
        sub_item6.Url = "#";

        ///->->EXAM. LABORATORIO
        List<Menu_Master> listSubMenu_6_1 = new List<Menu_Master>();
        Menu_Master sub_item6_1_1 = new Menu_Master();
        sub_item6_1_1.Id = "1";
        sub_item6_1_1.Nombre = "Exam. Laboratorio";
        sub_item6_1_1.Usuario = "alumno";
        sub_item6_1_1.Url = "/sigutc/academic/be/resLabsBeCovid.aspx";
        sub_item6_1_1.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item6_1_1);
        ///->->END EXAM. LABORATORIO

        sub_item6.SubMenu_Master = listSubMenu_6_1;
        listSubMenu_1.Add(sub_item6);
        ///->END BIENESTAR E.. 
        /// 

        ///->->MALLAS ACEDÉMICAS
        Menu_Master sub_item7 = new Menu_Master();
        sub_item7.Id = "1";
        sub_item7.Nombre = "Mallas académicas";
        sub_item7.Usuario = "alumno";
        sub_item7.Url = "/sigutc/academic/rpt/mallas.aspx";
        sub_item7.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item7);
        ///->->END MALLAS ACEDÉMICAS

        ///->->REPORTE DE CALIFICACIONES
        Menu_Master sub_item8 = new Menu_Master();
        sub_item8.Id = "1";
        sub_item8.Nombre = "Reporte de calificaciones";
        sub_item8.Usuario = "alumno";
        sub_item8.Url = "/sigutc/academic/private/v5/reportCalificaciones.aspx";
        sub_item8.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item8);
        ///->->END REPORTE DE CALIFICACIONES
        /// 

        ///->->HORARIOS
        Menu_Master sub_item9 = new Menu_Master();
        sub_item9.Id = "1";
        sub_item9.Nombre = "Horarios";
        sub_item9.Usuario = "alumno";
        sub_item9.Url = "/sigutc/academic/private/horarioCursos.aspx";
        sub_item9.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item9);
        ///->->END HORARIOS

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU ALUMNO

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Docente()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU DOCENTE
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Docente";
        item1.Usuario = "docente";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();

        ///->REPORTES
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Reportes...";
        sub_item1.Usuario = "docente";
        sub_item1.Url = "#";

        ///->->NOMINAS DE EXTUDUANTES
        List<Menu_Master> listSubMenu_1_1 = new List<Menu_Master>();
        Menu_Master sub_item1_1_1 = new Menu_Master();
        sub_item1_1_1.Id = "1";
        sub_item1_1_1.Nombre = "Nóminas de estudiantes";
        sub_item1_1_1.Usuario = "docente";
        sub_item1_1_1.Url = "/sigutc/academic/private/nominasAlumnos.aspx";
        sub_item1_1_1.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_1);
        ///->->END NOMINAS DE EXTUDUANTES

        ///->->REPORTE DE CALIFICACIONES
        Menu_Master sub_item1_1_2 = new Menu_Master();
        sub_item1_1_2.Id = "1";
        sub_item1_1_2.Nombre = "Reporte de calificaciones";
        sub_item1_1_2.Usuario = "docente";
        sub_item1_1_2.Url = "/sigutc/academic/private/v5/reportCalifDocente.aspx";
        sub_item1_1_2.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_2);
        ///->->END REPORTE DE CALIFICACIONES

        ///->->RESULTADOS EVAL DOCENTE
        Menu_Master sub_item1_1_3 = new Menu_Master();
        sub_item1_1_3.Id = "1";
        sub_item1_1_3.Nombre = "Resultados Eval docente";
        sub_item1_1_3.Usuario = "docente";
        sub_item1_1_3.Url = "/sigutc/academic/eval/resultados.aspx";
        sub_item1_1_3.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_3);
        ///->->END RESULTADOS EVAL DOCENTE

        ///->->MALLAS ACADEMICAS
        Menu_Master sub_item1_1_4 = new Menu_Master();
        sub_item1_1_4.Id = "1";
        sub_item1_1_4.Nombre = "Mallas académicas";
        sub_item1_1_4.Usuario = "docente";
        sub_item1_1_4.Url = "/sigutc/academic/rpt/mallas.aspx";
        sub_item1_1_4.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_4);
        ///->->END MALLAS ACADEMICAS

        sub_item1.SubMenu_Master = listSubMenu_1_1;
        listSubMenu_1.Add(sub_item1);
        ///->END REPORTES

        ///->DATOS PERSONALES
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Datos personales";
        sub_item2.Usuario = "docente";
        sub_item2.Url = "http://aplicaciones.utc.edu.ec/sith/th/updatepersonal";
        sub_item2.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item2);
        ///->END DATOS PERSONALES

        ///->DESEMPEÑO DOCENTE
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "3";
        sub_item3.Nombre = "Desempeño docente";
        sub_item3.Usuario = "docente";
        sub_item3.Url = "/sigutc/academic/eval/cuestionarioEval.aspx";
        sub_item3.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item3);
        ///->END DESEMPEÑO DOCENTE

        ///->INGRESO DE CALIFICACIONES
        Menu_Master sub_item4 = new Menu_Master();
        sub_item4.Id = "4";
        sub_item4.Nombre = "Ingreso de calificaciones";//dey
        sub_item4.Usuario = "docente";
        sub_item4.Url = "/sigutc/academic/private/v5/calificaciones.aspx";
        sub_item4.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item4);
        ///->END INGRESO DE CALIFICACIONES

        ///->INFORME GESTION DOCENTE
        Menu_Master sub_item5 = new Menu_Master();
        sub_item5.Id = "5";
        sub_item5.Nombre = "Informe gestión docente";
        sub_item5.Usuario = "docente";
        sub_item5.Url = "/sigutc/academic/igd/Formulario1.aspx";
        sub_item5.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item5);
        ///->END INFORME GESTION DOCENTE

        ///->HORARIO
        Menu_Master sub_item6 = new Menu_Master();
        sub_item6.Id = "6";
        sub_item6.Nombre = "Horario";
        sub_item6.Usuario = "docente";
        sub_item6.Url = "/sigutc/academic/private/horarioDocenteAlt.aspx";
        sub_item6.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item6);
        ///->END HORARIO

        ///->SILABO POSG..
        Menu_Master sub_item7 = new Menu_Master();
        sub_item7.Id = "1";
        sub_item7.Nombre = "Silabo Posg..";
        sub_item7.Usuario = "docente";
        sub_item7.Url = "#";

        ///->->PLANIFICAR SILABO
        List<Menu_Master> listSubMenu_7_1 = new List<Menu_Master>();
        Menu_Master sub_item7_1_1 = new Menu_Master();
        sub_item7_1_1.Id = "1";
        sub_item7_1_1.Nombre = "Planificar Sílabo";
        sub_item7_1_1.Usuario = "docente";
        sub_item7_1_1.Url = "/sigutc/academic/ss/ssPosg/planificacion.aspx";
        sub_item7_1_1.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item7_1_1);
        ///->->END PLANIFICAR SILABO

        ///->->REGISTRO ACTIVIDADES CLASE
        Menu_Master sub_item7_1_2 = new Menu_Master();
        sub_item7_1_2.Id = "1";
        sub_item7_1_2.Nombre = "Registro actividades clase";
        sub_item7_1_2.Usuario = "docente";
        sub_item7_1_2.Url = "/sigutc/academic/ss/actividadEjecutadaV2.aspx";
        sub_item7_1_2.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item7_1_2);
        ///->->END REGISTRO ACTIVIDADES CLASE

        ///->->REGISTRO DE OTRAS ACTIVIDADES DOCENTE
        Menu_Master sub_item7_1_3 = new Menu_Master();
        sub_item7_1_3.Id = "1";
        sub_item7_1_3.Nombre = "Registro de otras actividades docente";
        sub_item7_1_3.Usuario = "docente";
        sub_item7_1_3.Url = "/sigutc/academic/ss/asistenciaDocenteV2.aspx";
        sub_item7_1_3.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item7_1_3);
        ///->->END REGISTRO DE OTRAS ACTIVIDADES DOCENTE
        
        sub_item7.SubMenu_Master = listSubMenu_7_1;
        listSubMenu_1.Add(sub_item7);
        ///->END SILABO POSG..

        ///->SILABO..
        Menu_Master sub_item8 = new Menu_Master();
        sub_item8.Id = "1";
        sub_item8.Nombre = "Silabo..";
        sub_item8.Usuario = "docente";
        sub_item8.Url = "#";

        ///->->PLANIFICAR SILABO
        List<Menu_Master> listSubMenu_8_1 = new List<Menu_Master>();
        Menu_Master sub_item8_1_1 = new Menu_Master();
        sub_item8_1_1.Id = "1";
        sub_item8_1_1.Nombre = "Planificar Sílabo";
        sub_item8_1_1.Usuario = "docente";
        sub_item8_1_1.Url = "/sigutc/academic/ss/planificacion.aspx";
        sub_item8_1_1.SubMenu_Master = null;
        listSubMenu_8_1.Add(sub_item8_1_1);
        ///->->END PLANIFICAR SILABO

        ///->->REGISTRO ACTIVIDADES CLASE
        Menu_Master sub_item8_1_2 = new Menu_Master();
        sub_item8_1_2.Id = "1";
        sub_item8_1_2.Nombre = "Registro actividades clase";
        sub_item8_1_2.Usuario = "docente";
        sub_item8_1_2.Url = "/sigutc/academic/ss/actividadEjecutadaV2.aspx";
        sub_item8_1_2.SubMenu_Master = null;
        listSubMenu_8_1.Add(sub_item8_1_2);
        ///->->END REGISTRO ACTIVIDADES CLASE

        ///->->REGISTRO DE OTRAS ACTIVIDADES DOCENTE
        Menu_Master sub_item8_1_3 = new Menu_Master();
        sub_item8_1_3.Id = "1";
        sub_item8_1_3.Nombre = "Registro de otras actividades docente";
        sub_item8_1_3.Usuario = "docente";
        sub_item8_1_3.Url = "/sigutc/academic/ss/asistenciaDocenteV2.aspx";
        sub_item8_1_3.SubMenu_Master = null;
        listSubMenu_8_1.Add(sub_item8_1_3);
        ///->->END REGISTRO DE OTRAS ACTIVIDADES DOCENTE

        sub_item8.SubMenu_Master = listSubMenu_8_1;
        listSubMenu_1.Add(sub_item8);
        ///->END SILABO..

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU DOCENTE

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Academico()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU ACADEMICO
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Secretaría Acadm";
        item1.Usuario = "academico";
        item1.Url = "#";

        ///->MATERIAS
        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Materias...";
        sub_item1.Usuario = "academico";
        sub_item1.Url = "#";

        ///->->RETIRO ANULACIION
        List<Menu_Master> listSubMenu_1_1 = new List<Menu_Master>();
        Menu_Master sub_item1_1_1 = new Menu_Master();
        sub_item1_1_1.Id = "1";
        sub_item1_1_1.Nombre = "Retiro/Anulación";
        sub_item1_1_1.Usuario = "academico";
        sub_item1_1_1.Url = "/sigutc/academic/private/retiroMaterias.aspx";
        sub_item1_1_1.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_1);
        ///->->END RETIRO ANULACIION

        ///->->HOMOLOGACIONES
        Menu_Master sub_item1_1_2 = new Menu_Master();
        sub_item1_1_2.Id = "1";
        sub_item1_1_2.Nombre = "Homologaciones";
        sub_item1_1_2.Usuario = "academico";
        sub_item1_1_2.Url = "/sigutc/academic/private/homologacion.aspx";
        sub_item1_1_2.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_2);
        ///->->END HOMOLOGACIONES

        ///->->FECHA INICIO CARRERA 
        Menu_Master sub_item1_1_3 = new Menu_Master();
        sub_item1_1_3.Id = "1";
        sub_item1_1_3.Nombre = "Fecha inicio carrera (admin homol)";
        sub_item1_1_3.Usuario = "academico";
        sub_item1_1_3.Url = "/sigutc/academic/private/reguhomo.aspx";
        sub_item1_1_3.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_3);
        ///->->END FECHA INICIO CARRERA

        sub_item1.SubMenu_Master = listSubMenu_1_1;
        listSubMenu_1.Add(sub_item1);
        ///->END MATERIAS

        ///->REPORTES
        List<Menu_Master> listSubMenu_2 = new List<Menu_Master>();
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "1";
        sub_item2.Nombre = "Reportes...";
        sub_item2.Usuario = "academico";
        sub_item2.Url = "#";

        ///->->HISTORICO RETIRO
        List<Menu_Master> listSubMenu_2_1 = new List<Menu_Master>();

        Menu_Master sub_item1_2_1 = new Menu_Master();
        sub_item1_2_1.Id = "1";
        sub_item1_2_1.Nombre = "Histórico Retiros";
        sub_item1_2_1.Usuario = "academico";
        sub_item1_2_1.Url = "/sigutc/academic/private/histRetiros.aspx";
        sub_item1_2_1.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_1);
        ///->->END HISTORICO RETIRO

        ///->->RECORD ACADEM X MALLA
        Menu_Master sub_item1_2_2 = new Menu_Master();
        sub_item1_2_2.Id = "1";
        sub_item1_2_2.Nombre = "Record Academ x Malla";
        sub_item1_2_2.Usuario = "academico";
        sub_item1_2_2.Url = "/sigutc/academic/private/recordAcadPeriodo.aspx";
        sub_item1_2_2.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_2);
        ///->->END RECORD ACADEM X MALLA

        ///->->RECORD ACADEM X PERIODO
        Menu_Master sub_item1_2_3 = new Menu_Master();
        sub_item1_2_3.Id = "1";
        sub_item1_2_3.Nombre = "Record Academ x Periodo";
        sub_item1_2_3.Usuario = "academico";
        sub_item1_2_3.Url = "/sigutc/academic/private/recordAcadPensum.aspx";
        sub_item1_2_3.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_3);
        ///->->END RECORD ACADEM X PERIODO

        ///->->NOMINA DE ESTUDIANTES
        Menu_Master sub_item1_2_4 = new Menu_Master();
        sub_item1_2_4.Id = "1";
        sub_item1_2_4.Nombre = "Nómina de estudiantes";
        sub_item1_2_4.Usuario = "academico";
        sub_item1_2_4.Url = "/sigutc/academic/private/nominasAlumnos.aspx";
        sub_item1_2_4.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_4);
        ///->->END NOMINA DE ESTUDIANTES

        ///->->ESTUDIANTES PERDIDA DE GRAT
        Menu_Master sub_item1_2_5 = new Menu_Master();
        sub_item1_2_5.Id = "1";
        sub_item1_2_5.Nombre = "Estudiantes Pérdida de Grat.";
        sub_item1_2_5.Usuario = "academico";
        sub_item1_2_5.Url = "/sigutc/academic/private/reportGratuidad.aspx";
        sub_item1_2_5.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_5);
        ///->->END ESTUDIANTES PERDIDA DE GRAT

        ///->->RENDIMIENTO ADEM.
        Menu_Master sub_item1_2_6 = new Menu_Master();
        sub_item1_2_6.Id = "1";
        sub_item1_2_6.Nombre = "Rendimiento Adem.";
        sub_item1_2_6.Usuario = "academico";
        sub_item1_2_6.Url = "/sigutc/academic/private/prRendimiento.aspx";
        sub_item1_2_6.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_6);

        Menu_Master sub_item1_2_7 = new Menu_Master();
        sub_item1_2_7.Id = "1";
        sub_item1_2_7.Nombre = "Cruce de Horas.";
        sub_item1_2_7.Usuario = "academico";
        sub_item1_2_7.Url = "/sigutc/academic/private/ReportCruceHoras.aspx";
        sub_item1_2_7.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_7);
        ///->->END RENDIMIENTO ADEM.

        ///->->MATRIZ VALORES PERDIDA GRAT
        Menu_Master sub_item1_2_8 = new Menu_Master();
        sub_item1_2_8.Id = "1";
        sub_item1_2_8.Nombre = "Matriz Valores Pérdida Grat.";
        sub_item1_2_8.Usuario = "academico";
        sub_item1_2_8.Url = "/sigutc/academic/private/reportPerdidaGratuidad.aspx";
        sub_item1_2_8.SubMenu_Master = null;
        listSubMenu_2_1.Add(sub_item1_2_8);
        ///->->END MATRIZ VALORES PERDIDA GRAT

        sub_item2.SubMenu_Master = listSubMenu_2_1;
        listSubMenu_1.Add(sub_item2);
        ///->END REPORTES

        ///->GESTION
        List<Menu_Master> listSubMenu_3 = new List<Menu_Master>();
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "1";
        sub_item3.Nombre = "Gestión..";
        sub_item3.Usuario = "academico";
        sub_item3.Url = "#";

        ///->->CURSOS Y PARALELOS
        List<Menu_Master> listSubMenu_3_1 = new List<Menu_Master>();

        Menu_Master sub_item1_3_1 = new Menu_Master();
        sub_item1_3_1.Id = "1";
        sub_item1_3_1.Nombre = "(1) Cursos y Paralelos";
        sub_item1_3_1.Usuario = "academico";
        sub_item1_3_1.Url = "/sigutc/academic/private/cursoParalelo.aspx";
        sub_item1_3_1.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_1);
        ///->->END CURSOS Y PARALELOS

        ///->->DISTRIBUTIVO
        Menu_Master sub_item1_3_2 = new Menu_Master();
        sub_item1_3_2.Id = "1";
        sub_item1_3_2.Nombre = "(2) Distributivo";
        sub_item1_3_2.Usuario = "academico";
        sub_item1_3_2.Url = "/sigutc/academic/private/editDistributivo.aspx";
        sub_item1_3_2.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_2);
        ///->->END DISTRIBUTIVO

        ///->->HORARIOS
        Menu_Master sub_item1_3_3 = new Menu_Master();
        sub_item1_3_3.Id = "1";
        sub_item1_3_3.Nombre = "(3) Horario Académico";
        sub_item1_3_3.Usuario = "academico";
        sub_item1_3_3.Url = "/sigutc/academic/private/horarios.aspx";
        sub_item1_3_3.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_3);
        ///->->END HORARIOS

        ///->->HORARIOS OAD
        Menu_Master sub_item1_3_4 = new Menu_Master();
        sub_item1_3_4.Id = "1";
        sub_item1_3_4.Nombre = "(4) Horario OAD";
        sub_item1_3_4.Usuario = "academico";
        sub_item1_3_4.Url = "/sigutc/academic/private/confHorario.aspx";
        sub_item1_3_4.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_4);
        ///->->END HORARIOS OAD

        ///->->ADM CUPOS Y SERVICIOS
        Menu_Master sub_item1_3_5 = new Menu_Master();
        sub_item1_3_5.Id = "1";
        sub_item1_3_5.Nombre = "Adm Cupos y Servicios";
        sub_item1_3_5.Usuario = "academico";
        sub_item1_3_5.Url = "/sigutc/academic/private/admCuposVentanilla.aspx";
        sub_item1_3_5.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_5);
        ///->->END ADM CUPOS Y SERVICIOS

        ///->->CAMBIO DE PARALELO
        Menu_Master sub_item1_3_6 = new Menu_Master();
        sub_item1_3_6.Id = "1";
        sub_item1_3_6.Nombre = "Cambio de Paralelo";
        sub_item1_3_6.Usuario = "academico";
        sub_item1_3_6.Url = "/sigutc/academic/private/cambioParalelo.aspx";
        sub_item1_3_6.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_6);
        ///->->END CAMBIO DE PARALELO

        ///->->PERDIDA DE GRATUIDAD
        Menu_Master sub_item1_3_7 = new Menu_Master();
        sub_item1_3_7.Id = "1";
        sub_item1_3_7.Nombre = "Pérdida de Gratuidad";
        sub_item1_3_7.Usuario = "academico";
        sub_item1_3_7.Url = "/sigutc/academic/private/perdidaG.aspx";
        sub_item1_3_7.SubMenu_Master = null;
        listSubMenu_3_1.Add(sub_item1_3_7);
        ///->->END PERDIDA DE GRATUIDAD
        
        sub_item3.SubMenu_Master = listSubMenu_3_1;
        listSubMenu_1.Add(sub_item3);
        ///->END GESTION
        /// 

        ///->OTROS
        List<Menu_Master> listSubMenu_4 = new List<Menu_Master>();
        Menu_Master sub_item4 = new Menu_Master();
        sub_item4.Id = "1";
        sub_item4.Nombre = "Otros...";
        sub_item4.Usuario = "academico";
        sub_item4.Url = "#";

        ///->->CONFIGURAR EVALUADO(R)
        List<Menu_Master> listSubMenu_4_1 = new List<Menu_Master>();
        Menu_Master sub_item1_4_1 = new Menu_Master();
        sub_item1_4_1.Id = "1";
        sub_item1_4_1.Nombre = "Config. Evaluado(r)";
        sub_item1_4_1.Usuario = "academico";
        sub_item1_4_1.Url = "/sigutc/academic/eval/frmConfigEval.aspx";
        sub_item1_4_1.SubMenu_Master = null;
        listSubMenu_4_1.Add(sub_item1_4_1);
        ///->->END CONFIGURAR EVAL

        ///->->GENERAR CLAVE
        Menu_Master sub_item1_4_2 = new Menu_Master();
        sub_item1_4_2.Id = "1";
        sub_item1_4_2.Nombre = "Generar Clave";
        sub_item1_4_2.Usuario = "academico";
        sub_item1_4_2.Url = "/sigutc/academic/public/recoverypwd.aspx";
        sub_item1_4_2.SubMenu_Master = null;
        listSubMenu_4_1.Add(sub_item1_4_2);
        ///->->END GENERAR CLAVE
        /// 

        ///->->NUEVO ALUMNO
        Menu_Master sub_item1_4_3 = new Menu_Master();
        sub_item1_4_3.Id = "1";
        sub_item1_4_3.Nombre = "Nuevo Alumno";
        sub_item1_4_3.Usuario = "academico";
        sub_item1_4_3.Url = "/sigutc/academic/private/newalumno.aspx";
        sub_item1_4_3.SubMenu_Master = null;
        listSubMenu_4_1.Add(sub_item1_4_3);
        ///->->END NUEVO ALUMNO

        sub_item4.SubMenu_Master = listSubMenu_4_1;
        listSubMenu_1.Add(sub_item4);
        ///->END OTROS
        /// 

        ///->POSG..
        List<Menu_Master> listSubMenu_5 = new List<Menu_Master>();
        Menu_Master sub_item5 = new Menu_Master();
        sub_item5.Id = "1";
        sub_item5.Nombre = "POSG..";
        sub_item5.Usuario = "academico";
        sub_item5.Url = "#";

        ///->->AUDITORIA ACADEM
        List<Menu_Master> listSubMenu_5_1 = new List<Menu_Master>();
        Menu_Master sub_item1_5_1 = new Menu_Master();
        sub_item1_5_1.Id = "1";
        sub_item1_5_1.Nombre = "Auditoría academ";
        sub_item1_5_1.Usuario = "academico";
        sub_item1_5_1.Url = "/sigutc/academic/posg/posg_auditoriaAcademica.aspx";
        sub_item1_5_1.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_1);
        ///->->END AUDITORIA ACADEM

        ///->->NOTAS DE ADMISION
        Menu_Master sub_item1_5_2 = new Menu_Master();
        sub_item1_5_2.Id = "1";
        sub_item1_5_2.Nombre = "Notas admisión";
        sub_item1_5_2.Usuario = "academico";
        sub_item1_5_2.Url = "/sigutc/academic/posg/posg_notasAdmision.aspx";
        sub_item1_5_2.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_2);
        ///->->END NOTAS DE ADMISION

        ///->->REPORTE INSCRITOS
        Menu_Master sub_item1_5_3 = new Menu_Master();
        sub_item1_5_3.Id = "1";
        sub_item1_5_3.Nombre = "Reporte Inscritos";
        sub_item1_5_3.Usuario = "academico";
        sub_item1_5_3.Url = "/sigutc/academic/posg/posg_ReportInscripcion.aspx";
        sub_item1_5_3.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_3);
        ///->->END REPORTE INSCRITOS

        ///->->REPORTE MATRICULADOS
        Menu_Master sub_item1_5_4 = new Menu_Master();
        sub_item1_5_4.Id = "1";
        sub_item1_5_4.Nombre = "Reporte Matriculados";
        sub_item1_5_4.Usuario = "academico";
        sub_item1_5_4.Url = "/sigutc/academic/posg/posg_ReportMatricula.aspx";
        sub_item1_5_4.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_4);
        ///->->END REPORTE MATRICULADOS 

        ///->->AMIN. SILABO
        Menu_Master sub_item1_5_5 = new Menu_Master();
        sub_item1_5_5.Id = "1";
        sub_item1_5_5.Nombre = "Adm. Silabo";
        sub_item1_5_5.Usuario = "academico";
        sub_item1_5_5.Url = "/sigutc/academic/ss/ssPosg/new.aspx";
        sub_item1_5_5.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_5);
        ///->->END AMIN. SILABO

        ///->->PLANIFICACION
        Menu_Master sub_item1_5_6 = new Menu_Master();
        sub_item1_5_6.Id = "1";
        sub_item1_5_6.Nombre = "Planificación";
        sub_item1_5_6.Usuario = "academico";
        sub_item1_5_6.Url = "/sigutc/academic/ss/ssPosg/planificacion.aspx";
        sub_item1_5_6.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_6);
        ///->->END PLANIFICACION   

        ///->->VALIDAR (CERRAR)
        Menu_Master sub_item1_5_7 = new Menu_Master();
        sub_item1_5_7.Id = "1";
        sub_item1_5_7.Nombre = "Validar (Cerrar)";
        sub_item1_5_7.Usuario = "academico";
        sub_item1_5_7.Url = "/sigutc/academic/ss/ssPosg/validacion.aspx";
        sub_item1_5_7.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_7);
        ///->->END VALIDAR (CERRAR)
        ///

        ///->->Eliminar Pendientes POSG
        Menu_Master sub_item1_5_8 = new Menu_Master();
        sub_item1_5_8.Id = "1";
        sub_item1_5_8.Nombre = "Eliminar Pendientes POSG";
        sub_item1_5_8.Usuario = "academico";
        sub_item1_5_8.Url = "/sigutc/academic/posg/posg_eliminarPendiente.aspx";
        sub_item1_5_8.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_8);
        ///->->Eliminar Pendientes POSG

        ///->->Mejores Promedios - Certificados Admisión 
        Menu_Master sub_item1_5_9 = new Menu_Master();
        sub_item1_5_9.Id = "1";
        sub_item1_5_9.Nombre = "Mejores Promedios - Certificados Admisión";
        sub_item1_5_9.Usuario = "academico";
        sub_item1_5_9.Url = "/sigutc/academic/posg/posg_ReportNotas.aspx";
        sub_item1_5_9.SubMenu_Master = null;
        listSubMenu_5_1.Add(sub_item1_5_9);
        ///->->Mejores Promedios - Certificados Admisión

        sub_item5.SubMenu_Master = listSubMenu_5_1;
        listSubMenu_1.Add(sub_item5);
        ///->END POSG..
        ///


        ///->SILABO..
        Menu_Master sub_item6 = new Menu_Master();
        sub_item6.Id = "1";
        sub_item6.Nombre = "SILABO..";
        sub_item6.Usuario = "academico";
        sub_item6.Url = "#";

        ///->->ADM. SILABO
        List<Menu_Master> listSubMenu_6_1 = new List<Menu_Master>();
        Menu_Master sub_item1_6_1 = new Menu_Master();
        sub_item1_6_1.Id = "1";
        sub_item1_6_1.Nombre = "Adm. Silabo";
        sub_item1_6_1.Usuario = "academico";
        sub_item1_6_1.Url = "/sigutc/academic/ss/new.aspx";
        sub_item1_6_1.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_1);
        ///->->END ADM. SILABO 

        ///->->AURORIZAR PIQUES
        Menu_Master sub_item1_6_2 = new Menu_Master();
        sub_item1_6_2.Id = "1";
        sub_item1_6_2.Nombre = "Autorizar Piques";
        sub_item1_6_2.Usuario = "academico";
        sub_item1_6_2.Url = "/sigutc/academic/ss/autorizacionFuera.aspx";
        sub_item1_6_2.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_2);
        ///->->END AURORIZAR PIQUES 

        ///->->DASHBOARD
        Menu_Master sub_item1_6_3 = new Menu_Master();
        sub_item1_6_3.Id = "1";
        sub_item1_6_3.Nombre = "DashBoard_SS";
        sub_item1_6_3.Usuario = "academico";
        sub_item1_6_3.Url = "/sigutc/academic/ss/dashss.aspx";
        sub_item1_6_3.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_3);
        ///->->END DASHBOARD

        ///->->METODOLOGIAS
        Menu_Master sub_item1_6_4 = new Menu_Master();
        sub_item1_6_4.Id = "1";
        sub_item1_6_4.Nombre = "Metodologías";
        sub_item1_6_4.Usuario = "academico";
        sub_item1_6_4.Url = "/sigutc/academic/ss/metodologias.aspx";
        sub_item1_6_4.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_4);
        ///->->END METODOLOGIAS 

        ///->->PLANIFICACION
        Menu_Master sub_item1_6_5 = new Menu_Master();
        sub_item1_6_5.Id = "1";
        sub_item1_6_5.Nombre = "Planificación";
        sub_item1_6_5.Usuario = "academico";
        sub_item1_6_5.Url = "/sigutc/academic/ss/planificacion.aspx";
        sub_item1_6_5.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_5);
        ///->->END PLANIFICACION

        ///->->REAPERTURAR CLASE
        Menu_Master sub_item1_6_6 = new Menu_Master();
        sub_item1_6_6.Id = "1";
        sub_item1_6_6.Nombre = "Reaperturar clase";
        sub_item1_6_6.Usuario = "academico";
        sub_item1_6_6.Url = "/sigutc/academic/ss/justificacion.aspx";
        sub_item1_6_6.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_6);
        ///->->END REAPERTURAR CLASE  

        ///->->VALIDAR (CERRAR)
        Menu_Master sub_item1_6_7 = new Menu_Master();
        sub_item1_6_7.Id = "1";
        sub_item1_6_7.Nombre = "Validar (Cerrar)";
        sub_item1_6_7.Usuario = "academico";
        sub_item1_6_7.Url = "/sigutc/academic/ss/validacion.aspx";
        sub_item1_6_7.SubMenu_Master = null;
        listSubMenu_6_1.Add(sub_item1_6_7);
        ///->->END VALIDAR (CERRAR)

        sub_item6.SubMenu_Master = listSubMenu_6_1;
        listSubMenu_1.Add(sub_item6);
        ///->END SILABO..
        ///
        
        ///->MATRICULAS
        Menu_Master sub_item7 = new Menu_Master();
        sub_item7.Id = "2";
        sub_item7.Nombre = "Matriculas";
        sub_item7.Usuario = "academico";
        sub_item7.Url = "/sigutc/academic/private/matriculaOnLine.aspx";
        sub_item7.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item7);
        ///->END MATRICULAS

        ///->MODIFICAR CALIFICACIONES
        Menu_Master sub_item8 = new Menu_Master();
        sub_item8.Id = "3";
        sub_item8.Nombre = "Modificar calificaciones";
        sub_item8.Usuario = "docente";
        sub_item8.Url = "/sigutc/academic/private/v5/addUpdCalifV5.aspx";
        sub_item8.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item8);
        ///->END MODIFICAR CALIFICACIONES
        

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU ACADEMICO

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Evaluacion()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU EVALUACION
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Eval-Doc";
        item1.Usuario = "eval";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();

        ///->CONFIGURACION
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Configuración..";
        sub_item1.Usuario = "eval";
        sub_item1.Url = "#";

        ///->->FECHAS DE EJECUCION
        List<Menu_Master> listSubMenu_1_1 = new List<Menu_Master>();
        Menu_Master sub_item1_1_1 = new Menu_Master();
        sub_item1_1_1.Id = "1";
        sub_item1_1_1.Nombre = "Fechas de ejecución";
        sub_item1_1_1.Usuario = "eval";
        sub_item1_1_1.Url = "/sigutc/academic/private/calendarG.aspx";
        sub_item1_1_1.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_1);
        ///->->END FECHAS DE EJECUCION

        ///->->PREGUNTAS
        Menu_Master sub_item1_1_2 = new Menu_Master();
        sub_item1_1_2.Id = "1";
        sub_item1_1_2.Nombre = "Preguntas";
        sub_item1_1_2.Usuario = "eval";
        sub_item1_1_2.Url = "/sigutc/academic/eval/preguntas.aspx";
        sub_item1_1_2.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_2);
        ///->->END PREGUNTAS 

        ///->->
        Menu_Master sub_item1_1_3 = new Menu_Master();
        sub_item1_1_3.Id = "1";
        sub_item1_1_3.Nombre = "Adm. Cuestionarios";
        sub_item1_1_3.Usuario = "eval";
        sub_item1_1_3.Url = "/sigutc/academic/eval/cuestionarios.aspx";
        sub_item1_1_3.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_3);
        ///->->END 

        ///->->RESPUESTAS
        Menu_Master sub_item1_1_4 = new Menu_Master();
        sub_item1_1_4.Id = "1";
        sub_item1_1_4.Nombre = "Respuestas";
        sub_item1_1_4.Usuario = "eval";
        sub_item1_1_4.Url = "/sigutc/academic/eval/confgR.aspx";
        sub_item1_1_4.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_4);
        ///->->END RESPUESTAS

        ///->->ADM-CONFIG
        Menu_Master sub_item1_1_5 = new Menu_Master();
        sub_item1_1_5.Id = "1";
        sub_item1_1_5.Nombre = "Adm-Config. Evaluado(r)";
        sub_item1_1_5.Usuario = "eval";
        sub_item1_1_5.Url = "/sigutc/academic/eval/frmConfigEvalAdm.aspx";
        sub_item1_1_5.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_5);
        ///->->END ADM-CONFIG 

        ///->->VALIDACION DRECTIVOS
        Menu_Master sub_item1_1_6 = new Menu_Master();
        sub_item1_1_6.Id = "1";
        sub_item1_1_6.Nombre = "Validación Directivos";
        sub_item1_1_6.Usuario = "eval";
        sub_item1_1_6.Url = "/sigutc/academic/eval/directivos.aspx";
        sub_item1_1_6.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_6);
        ///->->END VALIDACION DRECTIVOS 

        ///->->PESO COMPONENTES
        Menu_Master sub_item1_1_7 = new Menu_Master();
        sub_item1_1_7.Id = "1";
        sub_item1_1_7.Nombre = "Peso Componentes";
        sub_item1_1_7.Usuario = "eval";
        sub_item1_1_7.Url = "/sigutc/academic/eval/pesoComponente.aspx";
        sub_item1_1_7.SubMenu_Master = null;
        listSubMenu_1_1.Add(sub_item1_1_7);
        ///->->END PESO COMPONENTES 
        
        sub_item1.SubMenu_Master = listSubMenu_1_1;
        listSubMenu_1.Add(sub_item1);
        ///->END CONFIGURACION 
        ///

        ///->CONFIG. EVALUADO(R)
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Config. Evaluado(r)";
        sub_item2.Usuario = "eval";
        sub_item2.Url = "/sigutc/academic/eval/frmConfigEval.aspx";
        sub_item2.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item2);
        ///->END CONFIG. EVALUADO(R) 

        ///->CUESTIONARIO
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "3";
        sub_item3.Nombre = "Cuestionarios";
        sub_item3.Usuario = "eval";
        sub_item3.Url = "/sigutc/academic/eval/cuestionarioEval.aspx";
        sub_item3.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item3);
        ///->END CUESTIONARIO

        ///->
        Menu_Master sub_item4 = new Menu_Master();
        sub_item4.Id = "4";
        sub_item4.Nombre = "Distributivo";
        sub_item4.Usuario = "eval";
        sub_item4.Url = "/sigutc/academic/private/editDistributivo.aspx";
        sub_item4.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item4);
        ///->END

        ///->GUIA
        Menu_Master sub_item5 = new Menu_Master();
        sub_item5.Id = "5";
        sub_item5.Nombre = "Guia";
        sub_item5.Usuario = "eval";
        sub_item5.Url = "/sigutc/academic/eval/Manual_Eval_Docente.pdf";
        sub_item5.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item5);
        ///->END GUIA

        ///->RESULTADOS
        Menu_Master sub_item6 = new Menu_Master();
        sub_item6.Id = "6";
        sub_item6.Nombre = "Resultados";
        sub_item6.Usuario = "eval";
        sub_item6.Url = "/sigutc/academic/eval/resultados.aspx";
        sub_item6.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item6);
        ///->END RESULTADOS

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU EVALUACION

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Publico()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU PUBLICO
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Reportes";
        item1.Usuario = "publico";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();

        ///->DASHBOARD
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Dashboard";
        sub_item1.Usuario = "publico";
        sub_item1.NoUsuario = "alumno";
        sub_item1.Url = "https://aplicaciones.utc.edu.ec/sigutc/academic/DashboardV2/Dashboard";
        sub_item1.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item1);
        ///->END DASHBOARD

        ///->DETALLE CALIFICACIONES
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Detalle calificaciones";
        sub_item2.Usuario = "publico";
        sub_item2.NoUsuario = "alumno";
        sub_item2.Url = "/sigutc/academic/rpt/CalifDetalle.aspx";
        sub_item2.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item2);
        ///->END DETALLE CALIFICACIONES

        ///->MALLAS
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "3";
        sub_item3.Nombre = "Mallas";
        sub_item3.Usuario = "publico";
        sub_item3.Url = "/sigutc/academic/rpt/mallas.aspx";
        sub_item3.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item3);
        ///->END MALLAS

        ///->SERVICIO DE REPORTES
        Menu_Master sub_item4 = new Menu_Master();
        sub_item4.Id = "4";
        sub_item4.Nombre = "Servicio de reportes";
        sub_item4.Usuario = "publico";
        sub_item4.NoUsuario = "alumno";
        sub_item4.Url = "http://172.16.34.50/ReportServer";
        sub_item4.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item4);
        ///->END SERVICIO DE REPORTES

        ///->INDICADORES IGD
        Menu_Master sub_item5 = new Menu_Master();
        sub_item5.Id = "5";
        sub_item5.Nombre = "Indicadores IGD";
        sub_item5.Usuario = "publico";
        sub_item5.NoUsuario = "alumno";
        sub_item5.Url = "/sigutc/academic/igd/indicadoresIGD.aspx";
        sub_item5.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item5);
        ///->END INDICADORES IGD

        ///->SEGUIEMIENTO SILABO
        Menu_Master sub_item6 = new Menu_Master();
        sub_item6.Id = "6";
        sub_item6.Nombre = "Seguimiento sílabo";
        sub_item6.Usuario = "publico";
        sub_item6.NoUsuario = "alumno";
        sub_item6.Url = "/sigutc/academic/ss/reportCalidad.aspx";
        sub_item6.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item6);
        ///->END SEGUIEMIENTO SILABO


        ///->POSG
        Menu_Master sub_item7 = new Menu_Master();
        sub_item7.Id = "1";
        sub_item7.Nombre = "POSG";
        sub_item7.Usuario = "publico";
        sub_item7.Url = "#";

        ///->->DETALLE DE CALIFICACIONES
        List<Menu_Master> listSubMenu_7_1 = new List<Menu_Master>();
        Menu_Master sub_item1_7_1 = new Menu_Master();
        sub_item1_7_1.Id = "1";
        sub_item1_7_1.Nombre = "Detalle calificaciones";
        sub_item1_7_1.Usuario = "publico";
        sub_item1_7_1.Url = "/sigutc/academic/rpt/CalifDetalle.aspx";
        sub_item1_7_1.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item1_7_1);
        ///->->END DETALLE DE CALIFICACIONES

        ///->->REPORTE INSCRITOS
        Menu_Master sub_item1_7_2 = new Menu_Master();
        sub_item1_7_2.Id = "2";
        sub_item1_7_2.Nombre = "Reporte inscritos";
        sub_item1_7_2.Usuario = "publico";
        sub_item1_7_2.NoUsuario = "alumno";
        sub_item1_7_2.Url = "/sigutc/academic/posg/posg_ReportInscripcion.aspx";
        sub_item1_7_2.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item1_7_2);
        ///->->END REPORTE INSCRITOS 

        ///->->REPORTE MATRICULADOS
        Menu_Master sub_item1_7_3 = new Menu_Master();
        sub_item1_7_3.Id = "3";
        sub_item1_7_3.Nombre = "Reporte matriculados";
        sub_item1_7_3.Usuario = "publico";
        sub_item1_7_3.NoUsuario = "alumno";
        sub_item1_7_3.Url = "/sigutc/academic/posg/posg_ReportMatricula.aspx";
        sub_item1_7_3.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item1_7_3);
        ///->->ENDREPORTE MATRICULADOS 

        ///->->REPORTE RECAUDACION
        Menu_Master sub_item1_7_4 = new Menu_Master();
        sub_item1_7_4.Id = "4";
        sub_item1_7_4.Nombre = "Reporte recaudación";
        sub_item1_7_4.Usuario = "publico";
        sub_item1_7_4.NoUsuario = "alumno";
        sub_item1_7_4.Url = "/sigutc/academic/posg/posg_ReportPagoPosgrado.aspx";
        sub_item1_7_4.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item1_7_4);
        ///->->END REPORTE RECAUDACION 

        ///->->REPORTE COLEGIATURAS
        Menu_Master sub_item1_7_5 = new Menu_Master();
        sub_item1_7_5.Id = "5";
        sub_item1_7_5.Nombre = "Reporte Colegiaturas";
        sub_item1_7_5.Usuario = "publico";
        sub_item1_7_5.NoUsuario = "alumno";
        sub_item1_7_5.Url = "/sigutc/academic/posg/posg_ReportPagoColegiaturas.aspx";
        sub_item1_7_5.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item1_7_5);
        ///->->END REPORTE COLEGIATURAS 

        ///->->POSTULANTES DOCENTES
        Menu_Master sub_item1_7_6 = new Menu_Master();
        sub_item1_7_6.Id = "6";
        sub_item1_7_6.Nombre = "Postulantes Docentes";
        sub_item1_7_6.Usuario = "publico";
        sub_item1_7_6.NoUsuario = "alumno";
        sub_item1_7_6.Url = "/sigutc/academic/posg/postulacionPosg.aspx";
        sub_item1_7_6.SubMenu_Master = null;
        listSubMenu_7_1.Add(sub_item1_7_6);
        ///->->END POSTULANTES DOCENTES
         
        sub_item7.SubMenu_Master = listSubMenu_7_1;
        listSubMenu_1.Add(sub_item7);
        ///->END POSG
        ///

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU PUBLICO

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Titulacion()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU TITULACION
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Titulación";
        item1.Usuario = "titulacion";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();

        ///->TITULACION
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Titulación";
        sub_item1.Usuario = "titulacion";
        sub_item1.Url = "/sigutc/academic/private/titulacion.aspx";
        sub_item1.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item1);
        ///->END TITULACION

        ///->TITULACION SG
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Titulación SG";
        sub_item2.Usuario = "titulacion";
        sub_item2.Url = "/sigutc/academic/private/titulacionSG.aspx";
        sub_item2.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item2);
        ///->END TITULACION SG

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU TITULACION

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Tesoreria()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();
        //////MENU TESORERIA U
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Tesoreria u";
        item1.Usuario = "tesoreria";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();

        ///->PAGO INSCRIPCIONES
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Pago inscripciones";
        sub_item1.Usuario = "tesoreria";
        sub_item1.Url = "/sigutc/academic/posg/posg_pagoInscripcion.aspx";
        sub_item1.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item1);
        ///->END PAGO INSCRIPCIONES

        ///->PAGO MATRICULA
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Pago matrícula";
        sub_item2.Usuario = "tesoreria";
        sub_item2.Url = "/sigutc/academic/posg/posg_pagoMatricula.aspx";
        sub_item2.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item2);
        ///->END PAGO MATRICULA

        ///->RECAUDACION
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "3";
        sub_item3.Nombre = "Recaudación";
        sub_item3.Usuario = "tesoreria";
        sub_item3.Url = "/sigutc/academic/posg/posg_pagoPosgrado.aspx";
        sub_item3.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item3);
        ///->END RECAUDACION

        ///->CARGA MASIVA
        Menu_Master sub_item4 = new Menu_Master();
        sub_item4.Id = "4";
        sub_item4.Nombre = "Carga masiva";
        sub_item4.Usuario = "tesoreria";
        sub_item4.Url = "/sigutc/academic/posg/posg_cargaPagosExcel.aspx";
        sub_item4.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item4);
        ///->END CARGA MASIVA

        ///->REPORTE COLEGIATURAS
        Menu_Master sub_item5 = new Menu_Master();
        sub_item5.Id = "5";
        sub_item5.Nombre = "Reporte Colegiaturas";
        sub_item5.Usuario = "tesoreria";
        sub_item5.Url = "/sigutc/academic/posg/posg_ReportPagoColegiaturas.aspx";
        sub_item5.SubMenu_Master = null;
        listSubMenu_1.Add(sub_item5);
        ///->END REPORTE COLEGIATURAS

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU TESORERIA

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Calendario()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();

        //////MENU CALENDARIO
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Calendario..";
        item1.Usuario = "administrador";
        item1.Url = "#";

        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();

        if (Roles.IsUserInRole(Context.User.Identity.Name, "PERIODOG_CALENDAR"))
        {
            ///->GENERAL
            Menu_Master sub_item1 = new Menu_Master();
            sub_item1.Id = "1";
            sub_item1.Nombre = "General";
            sub_item1.Usuario = "administrador";
            sub_item1.Url = "/academic/private/periodoGE";
            listSubMenu_1.Add(sub_item1);
            ///->END GENERAL
        }

        if (Roles.IsUserInRole(Context.User.Identity.Name, "PERIODOAC_CALENDAR"))
        {
            ///->ACADÉMICO
            Menu_Master sub_item2 = new Menu_Master();
            sub_item2.Id = "2";
            sub_item2.Nombre = "Académico";
            sub_item2.Usuario = "administrador";
            sub_item2.Url = "/academic/private/periodoAC";
            listSubMenu_1.Add(sub_item2);
            ///->END ACADÉMICO
        }

        if (Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_SS"))
        {
            ///->SÍLABO
            Menu_Master sub_item3 = new Menu_Master();
            sub_item3.Id = "3";
            sub_item3.Nombre = "Sílabo";
            sub_item3.Usuario = "administrador";
            sub_item3.Url = "/academic/private/periodoSL";
            listSubMenu_1.Add(sub_item3);
            ///->END SÍLABO
        }

        if (Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_MATRICULAS"))
        {
            ///->MATRÍCULA
            Menu_Master sub_item4 = new Menu_Master();
            sub_item4.Id = "4";
            sub_item4.Nombre = "Matrícula";
            sub_item4.Usuario = "administrador";
            sub_item4.Url = "/academic/private/periodoMA";
            listSubMenu_1.Add(sub_item4);
            ///->END MATRÍCULA
        }

        if (Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_CALIF"))
        {
            ///->CALIFICACIONES
            Menu_Master sub_item5 = new Menu_Master();
            sub_item5.Id = "5";
            sub_item5.Nombre = "Calificaciones";
            sub_item5.Usuario = "administrador";
            sub_item5.Url = "/academic/private/periodoCA";
            listSubMenu_1.Add(sub_item5);
            ///->END CALIFICACIONES
        }

        if (Roles.IsUserInRole(Context.User.Identity.Name, "CALENDAR_IDIOMAS"))
        {
            ///->IDIOMAS
            Menu_Master sub_item6 = new Menu_Master();
            sub_item6.Id = "6";
            sub_item6.Nombre = "Idiomas";
            sub_item6.Usuario = "administrador";
            sub_item6.Url = "/academic/private/periodoID";
            listSubMenu_1.Add(sub_item6);
            ///->END IDIOMAS
        }

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU CALENDARIO

        return listMenu;
    }

    private List<Menu_Master> ListMenu_Gestion_Academica()
    {
        List<Menu_Master> listMenu = new List<Menu_Master>();

        //////MENU GESTIÓN ACADÉMICA
        Menu_Master item1 = new Menu_Master();
        item1.Id = "1";
        item1.Nombre = "Gestión Académica";
        item1.Usuario = "academico";
        item1.Url = "#";

        ///->GESTIÓN DE SEDE
        List<Menu_Master> listSubMenu_1 = new List<Menu_Master>();
        Menu_Master sub_item1 = new Menu_Master();
        sub_item1.Id = "1";
        sub_item1.Nombre = "Sede";
        sub_item1.Usuario = "academico";
        sub_item1.Url = "/academic/private/sede.aspx";
        listSubMenu_1.Add(sub_item1);
        ///->END GESTIÓN DE SEDE

        ///->GESTIÓN DE FACULTAD
        Menu_Master sub_item2 = new Menu_Master();
        sub_item2.Id = "2";
        sub_item2.Nombre = "Facultad";
        sub_item2.Usuario = "academico";
        sub_item2.Url = "/academic/private/facultad.aspx";
        listSubMenu_1.Add(sub_item2);
        ///->END GESTIÓN DE FACULTAD

        ///->GESTIÓN DE CARRERA
        Menu_Master sub_item3 = new Menu_Master();
        sub_item3.Id = "2";
        sub_item3.Nombre = "Carrera";
        sub_item3.Usuario = "academico";
        sub_item3.Url = "/academic/private/carrera.aspx";
        listSubMenu_1.Add(sub_item3);
        ///->END GESTIÓN DE CARRERA

        item1.SubMenu_Master = listSubMenu_1;
        listMenu.Add(item1);
        //////END MENU GESTIÓN ACADÉMICA

        return listMenu;
    }

    private string Menu_CD(Menu_Master menu)
    {
         var contenido_menu = @"<li class='pcoded-hasmenu'>
                                          <a href='javascript:void(0)' class='waves-effect waves-dark'>
                                            <span class='pcoded-micon'><i class='ti-layout-grid2-alt'></i></span>
                                            <span class='pcoded-mtext' data-i18n='nav.basic-components.main'>"+menu.Nombre+@"</span>
                                            <span class='pcoded-mcaret'></span>
                                        </a>";
        if (menu.SubMenu_Master != null) {
            contenido_menu = string.Concat(contenido_menu, @"<ul class='pcoded-submenu'>");
            foreach (var item in menu.SubMenu_Master)
            {
                if (item.SubMenu_Master != null)
                {
                    contenido_menu = string.Concat(contenido_menu, Menu_CD(item));
                }
                else
                {
                    bool validarNoUsuario = true;

                    if (item.NoUsuario != null)
                    {
                        if (Roles.IsUserInRole(Context.User.Identity.Name, "ALUMNO"))
                        {
                            if (item.NoUsuario.Equals("alumno"))
                            {
                                validarNoUsuario = false;
                            }
                        }
                    }

                    if (validarNoUsuario)
                    {
                        contenido_menu = string.Concat(contenido_menu, @"<li class=''>
                                                                    <a href='" + item.Url + @"' class='waves-effect waves-dark'>
                                                                        <span class='pcoded-micon'><i class='ti-angle-right'></i></span>
                                                                        <span class='pcoded-mtext' data-i18n='nav.basic-components.alert'>" + item.Nombre + @"</span>
                                                                        <span class='pcoded-mcaret'></span>
                                                                    </a>
                                                                </li>");
                    }
                }
            }
            contenido_menu = string.Concat(contenido_menu, @"</ul>");
        }
        contenido_menu = string.Concat(contenido_menu, @"</li>");
        return contenido_menu;
    }

    //private bool IsSeretarioAcademico(string comodin, string usr1, string sede1, string facultad1, string rol1)// devuelve si es secretario academico este perfil es el único que permite ejecutar este formulario de matriculas con todo sus privilegios
    //{
    //    ClassSedeFacCarr sfc = new ClassSedeFacCarr();
    //    bool retorno = false;
    //    switch (comodin)
    //    {
    //        case "isSecretaryAcademicFacultad":// determina si es un secretario académico               
    //            var listUsr = Roles.GetRolesForUser(usr1);
    //            int aux = 0;
    //            for (int i = 0; i < listUsr.Length; i++)
    //            {
    //                if (listUsr[i].IndexOf("_") >= 0)
    //                {
    //                    aux++;
    //                }
    //            }
    //            if (aux == 0)
    //            {
    //                break;
    //            }
    //            for (int i = 0; i < listUsr.Length; i++)
    //            {

    //                if (listUsr[i].IndexOf("_") >= 0)
    //                {
    //                    if (sfc.GetFacultad(usr1, listUsr[i].Substring(0, listUsr[i].IndexOf("_")), true).Count > 0)
    //                    {
    //                        retorno = true;
    //                        break;
    //                    }
    //                }
    //            }

    //            break;
    //        case "isSecretaryAcademicCarrera":// determina si una carrera seleccionada(ejm: MUTC_CIYA_IISC) por medio del secretario académico, tiene permiso para proceder a configurar la matricula caso contrario lo negará
    //            var listCarrera = sfc.GetCarrera(usr1, sede1, facultad1, true);
    //            for (int i = 0; i < listCarrera.Count; i++)
    //            {
    //                if (string.Concat(listCarrera[i].strcod_sede, "_", listCarrera[i].strcod_fac, "_", listCarrera[i].strcod_car) == rol1)
    //                {
    //                    retorno = true;
    //                    break;
    //                }
    //            }
    //            break;
    //    }


    //    return retorno;
    //}
}

public class Menu_Master
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Usuario { get; set; }
    public string Url { get; set; }
    public List<Menu_Master> SubMenu_Master { get; set; }
    public string NoUsuario { get; set; }
    public Menu_Master() { }
    public Menu_Master(string id, string nombre, string usuario, string nousuario, string url, bool submenu=false)
    {
        Id = id;
        Nombre = nombre;
        Usuario = usuario;
        NoUsuario = nousuario;
        Url = url;
        if (submenu)
        {
            SubMenu_Master = new List<Menu_Master>();
        }
        else
        {
            SubMenu_Master = null;
        }        
    }

    public void AddSubMenu(Menu_Master subMenu)
    {
        if (SubMenu_Master != null)
        {
            SubMenu_Master.Add(subMenu);
        }
    }
}