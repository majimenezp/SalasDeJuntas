/// <reference path="libs/jquery-1.10.2.min.js" />
/// <reference path="libs/backbone.js"/>
/// <reference path="libs/underscore.js"/>
/// <reference path="libs/handlebars.js"/>
var fecha = new Date();
moment.lang("es");
var modelos = {};
var vistas = {};
var App = null;
var colorActual=0;
var listaColores = [
    "rgba(255, 92, 0, 0.75)",
    "rgba(139, 234, 0, 0.75)",
    "rgba(8, 111, 161, 0.75)",
    "rgba(255, 133, 64, 0.75)",
    "rgba(170, 245, 61, 0.75)",
    "rgba(60, 160, 208, 0.75)",
    "rgba(255, 165, 115, 0.75)",
    "rgba(190, 245, 110, 0.75)",
    "rgba(99, 173, 208, 0.75)",
    "rgba(166, 60, 0, 0.75)",
    "rgba(90, 152, 0, 0.75)",
    "rgba(3, 71, 105, 0.75)"
];

window.Handlebars.registerHelper('setPos', function (value, options) {
    var posInicio = $("#" + ObtenerRenglonHora(value.HoraInicioJs)).offset();
    var posFin = $("#" + ObtenerRenglonHora(value.HoraFinJs)).offset();
    var posCon = $("#ListaHoras").offset();
   
    return "<div class='cajaJunta' style='top:" + (posInicio.top - posCon.top) +
        "px;height:" + (posFin.top - posInicio.top) + "px;background-color:"+ ObtenerColor()+"'>" + options.fn(this) + "</div>";
});

function ObtenerRenglonHora(hora) {
    var idHora = hora.replace(/:/g, "");
    var parteHora = parseInt(idHora.substring(0, 2));
    var parteMinutos = parseInt(idHora.substring(2));
    if (parteMinutos != 30 || parteMinutos != 0) {
        if (parteMinutos<21) {
            parteMinutos = 0;
        }
        if (parteMinutos > 20 && parteMinutos<45) {
            parteMinutos = 30;
        }
        if (parteMinutos >= 45 && parteMinutos < 60) {
            parteHora += 1;
            parteMinutos = 0;
        }
        return pad(parteHora, 2) + pad(parteMinutos, 2);
    }
}

function ObtenerColor() {
    var color = listaColores[colorActual]
    colorActual += 1;
    if (colorActual == listaColores.length) {
        colorActual = 0;
    }
    return color;
}

function pad(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

function InicioBackbone() {
    modelos.Junta = Backbone.RelationalModel.extend({
        urlRoot: "/api/juntas",
        idAttribute: "Id",
    });

    modelos.Dia = Backbone.RelationalModel.extend({
        urlRoot: "/api/dias",
        idAttribute: "Id",
        relations: [{
            type: Backbone.HasMany,
            key: 'juntas',
            autoFetch: true,
            relatedModel: 'modelos.Junta',
            //reverseRelation: {
            //    key: 'dia',
            //    includeInJSON: 'Id',
            //},
        }]
    });

    modelos.Dias = Backbone.Collection.extend({
        url: "/api/dias",
        model: modelos.Dia
    });

    //-------------------Inician vistas backbone------------------------------------------
    vistas.Junta = Backbone.View.extend({
        tagName: 'div',
        el: $('#ListaHoras'),
        className: 'Cajajunta',
        initialize: function () {
            _.bindAll(this, 'render');
            this.model.bind('change', this.render);
        },
        template: Handlebars.compile($('#TemplateCajaJunta').html()),
        render: function () {
            $(this.el).find(".cajaJunta").remove();
            return $(this.el).append(this.template(this.model.toJSON()));
        },
    });

    //-------------------Terminan vistas backbone------------------------------------------


    var router = Backbone.Router.extend({
        routes: {
            "dias/:Id/:IdSala": "mostrar_dia",
        },
        mostrar_dia: function (Id, IdSala) {
            colorActual = 0;
            var diaActual = modelos.Dia.findOrCreate({ Id: Id});
            var vistaJunta = new vistas.Junta({model: diaActual });
            //diaActual.get("juntas");
            diaActual.fetch({ data: $.param({ IdSala: IdSala }) });
        }
    });


    App = new router();
    Backbone.history.start({ pushState: true });
}


//---------------------------Termina backbone---------------------------------------------
$(document).ready(function () {
    InicioBackbone();
    PonerFechaActual();
    $(".ayer").click(mostrarAnterior);
    $(".hoy").click(mostrarHoy);
    $(".manania").click(mostrarSiguiente);
});


function PonerFechaActual() {
    App.mostrar_dia(moment(fecha).format("DDMMYYYY"),idSala);
    $(".DiaActual").html(moment(fecha).format("D"));
    $(".MesActual").html(moment(fecha).format("MMMM"));
    $(".FechaActual").html(moment(fecha).format("dddd, D [de] MMMM [del] YYYY"));
}

function mostrarAnterior() {
    fecha=moment(fecha).subtract('d', 1);
    PonerFechaActual();
}

function mostrarHoy() {
    fecha = new Date();
    PonerFechaActual();
}

function mostrarSiguiente() {
    fecha = moment(fecha).add('d', 1);
    PonerFechaActual();
}

//$("#ListaHoras>div").css({top:$("#1230").offset().top -$("#ListaHoras").offset().top +"px"})