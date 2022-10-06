var ferias

$(document).ready(function ()
{
    mostrarGoogleMaps(9.888769, -84.072876);
})

function mostrarGoogleMaps(val1,val2)
{
	//Creamos el punto a partir de las coordenadas:
	punto = new google.maps.LatLng(val1, val2);
	//Configuramos las opciones indicando Zoom, punto(el que hemos creado) y tipo de mapa
	var myOptions = 
	{
		zoom: 7, center: punto, mapTypeId: google.maps.MapTypeId.ROADMAP
	};
	//Creamos el mapa e indicamos en qué caja queremos que se muestre
	var map = new google.maps.Map(document.getElementById("mapa"), myOptions);

	var image = {
	    url: '../images/feriaR.png',
	    // This marker is 20 pixels wide by 32 pixels high.
	    size: new google.maps.Size(32, 37),
	    // The origin for this image is (0, 0).
	    origin: new google.maps.Point(0, 0),
	    // The anchor for this image is the base of the flagpole at (0, 32).
	    anchor: new google.maps.Point(16, 34)
	};

	var image2 = {
	    url: '../images/feria.png',
	    // This marker is 20 pixels wide by 32 pixels high.
	    size: new google.maps.Size(32, 37),
	    // The origin for this image is (0, 0).
	    origin: new google.maps.Point(0, 0),
	    // The anchor for this image is the base of the flagpole at (0, 32).
	    anchor: new google.maps.Point(16, 34)
	};

    //Opcionalmente podemos mostrar el marcador en el punto que hemos creado.
	
        
	    var markup = new google.maps.Marker(
	    {
	        position: punto,
	        map: map,
	        draggable: true,
	        title: "Arrastre a la feria",
	        icon: image
	    });
	

	    markup.setAnimation(google.maps.Animation.BOUNCE);

	    google.maps.event.addListener(markup, 'drag', function () {
	        $("#Fa_Latitud").val(markup.getPosition().lat());
	        $("#Fa_longitud").val(markup.getPosition().lng());
	        markup.setAnimation(null);
	        markup.setIcon(image2);
	    });

	

}

function BuscarCantones() {
    var Pro_id = $('#Provincias_Pro_id').val();
    if (Pro_id != "")
    {
        $.ajax(
        {
            url: '/Ferias_Agricolas/BuscarCantones',
            type: "GET",
            dataType: "JSON",
            data: { idProvincia: Pro_id },
            success: function (selectCantones) {
                $("#Cantones_cnt_id").html(""); // clear before appending new list
                $("#Cantones_cnt_id").append($('<option></option>').val("").html("Seleccione un canton:"));
                $.each(selectCantones, function (i, canton) {
                    $("#Cantones_cnt_id").append($('<option></option>').val(canton.Value).html(canton.Text));
                });
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    }
    else
    {
        $("#Cantones_cnt_id").html("");
        $("#Cantones_cnt_id").append($('<option></option>').val("").html("Seleccione un canton:"));
        $("#Fa_Distrito").html("");
        $("#Fa_Distrito").append($('<option></option>').val("").html("Seleccione un distrito:"));
    }
}

function BuscarDistritos() {
    var Cant_id = $('#Cantones_cnt_id').val();
    if (Cant_id != "")
    {
        $.ajax(
        {
            url: '/Ferias_Agricolas/BuscarDistritos',
            type: "GET",
            dataType: "JSON",
            data: { idCanton: Cant_id },
            success: function (selectDistritos) {
                $("#Fa_Distrito").html(""); // clear before appending new list
                $("#Fa_Distrito").append($('<option></option>').val("").html("Seleccione un distrito:"));
                $.each(selectDistritos, function (i, distrito) {
                    $("#Fa_Distrito").append($('<option></option>').val(distrito.Value).html(distrito.Text));
                });
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    }
    else {
        $("#Fa_Distrito").html("");
        $("#Fa_Distrito").append($('<option></option>').val("").html("Seleccione un distrito:"));
    }
}