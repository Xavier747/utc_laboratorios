<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNuevo.master" AutoEventWireup="true" CodeFile="ReservaLaboratorioLab.aspx.cs" Inherits="academic_private_reservalab_ReservaLaboratorioLab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
      <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {
                buttonText: {
                    month: 'Mes',
                    week: 'Semana',
                    list: 'Lista'
                },
                initialView: 'dayGridMonth', // vista mensual
                locale: 'es',                // idioma español
                headerToolbar: {
                    left: 'prev,next',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,listWeek'
                },
                dayHeaderContent: function (arg) {
                    switch (arg.date.getUTCDay()) {
                        case 0: return 'Domingo';
                        case 1: return 'Lunes';
                        case 2: return 'Martes';
                        case 3: return 'Miércoles';
                        case 4: return 'Jueves';
                        case 5: return 'Viernes';
                        case 6: return 'Sábado';
                    }
                },
            });

            calendar.render();
        });
    </script>
</asp:Content>

