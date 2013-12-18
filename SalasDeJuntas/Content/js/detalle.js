/// <reference path="libs/jquery-1.10.2.min.js" />
/// <reference path="libs/backbone.js"/>
/// <reference path="libs/underscore.js"/>
/// <reference path="libs/handlebars.js"/>
var fecha = new Date();
moment.lang("es");
var modelos = {};
var vistas = {};
var App = null;

window.Handlebars.registerHelper('setPos', function (value, options) {
    var pos1 = $("#" + value.HoraInicioJs.replace(/:/g, "")).offset();
    var pos2 = $("#ListaHoras").offset();
    console.log(pos1);
    //var $el = $('').html(options.fn(this));
    //var cambios = $el.find('[value=' + value + ']').attr({ 'selected': 'selected' });
    //if (cambios.length == 0) {
    //    $el.find(":first").attr({ 'selected': 'selected' });
    //}
    return "<div class='cajaJunta' style='top:" + (pos1.top-pos2.top) + "px;'>" + options.fn(this) + "</div>";
});


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