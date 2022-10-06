

$(document).ready(function () {
    
    $('.chosen-select').chosen();
    $("#tipoProductos_chosen").width("100%");
    $("#tipoProductos_chosen").css("margin","6px");
    $("#tipoProductos").change();
    $("#cmb_cesta_chosen").width("100%");
    $("#cmb_cesta_chosen").css("margin", "6px");
    $("#cmb_cesta").change();
    

    var valores = $('#cmb_cesta option').length;

    $("#panelFiltros").css("display","block")
    //var options = $('#cmb_cesta').attr('options').length;
    /*for (index = 0; index < $("#cmb_cesta").length; index++) {
        if ($("#cmb_cesta")[index].value == Value) SelectObject.selectedIndex = index;
    }*/
})

$(".btnComprar").click(function () {
    id = $(this).attr("id").split("_")[1]
    if ($("#cmbcesta").val() != "null") {
        
        valor = $("#txt_" + id).val()
        if (valor > 0) {
            //alert($("#cmbcesta").val());
            $.ajax(
            {
                url: '/Compras/Comprar',
                type: "GET",
                dataType: "JSON",
                data:
                {
                    id_canasta: $("#cmbcesta").val(),
                    id_producto_por_agricultor: id,
                    cantidad: valor
                },
                success: function (respuesta) {
                    if (respuesta == "exito") {
                        $("#txt_" + id).val("")
                        alert("Compra registrada")
                    }
                    else {
                        alert("Error al comprar")
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.responseText);
                }
            });
        }
        else
        {
            if (valor == "")
                alert("Ingrese la cantidad que desea comprar.");
            else {
                $("#txt_" + id).val("")
                alert("La cantidad solicitada debe de ser mayor que uno.")
            }
        }
    }
    else
    {
        $("#txt_" + id).val("")
        alert("Debe de agregar una cesta antes de comprar.")
    }
});

$("#filterForm").submit(function ()
{
    var encontrado = false;
    $('#cmb_cesta option').each(function () {
        if (encontrado == false)
        {
            if($(this).html() == $("#txtcesta").val())
            {
                encontrado = true
            }
        }
    })
    
    if (encontrado == false)
    {
        return true
    }
    else
    {
        alert("Ya existe una cesta con este nombre")
        return false
    }

    
})

$("#btncrear").click(function () {

    if ($("#txtcesta").val() =="")
    {
        alert("Escriba el nombre de la cesta");
    }
    else
    {
        $("#filterForm").submit()
    }
})

$("#tipoProductos").change(function () {
    var pro = $("#tipoProductos").val()+""
    pro = pro.replace(",", " ");
    if (pro == "null")
    {
        pro = "";
    }

    $("#Filtro").val(pro);
});

$("#cmb_cesta").change(function () {
    $("#cmbcesta").val($("#cmb_cesta").val() + "")

    if ($("#cmbcesta").val() != "null")
    {
        $("#btnDetalles").prop('disabled', false);
    }
    else
    {
        $("#btnDetalles").prop('disabled', true);
    }
})

$("#btnDetalles").click(function () {

    $("#tituloModal").html($("#cmb_cesta option").html() + "")
    
    procesoDetalles()

    $('#myModal').modal('show');
});

function procesoDetalles()
{
    $.ajax(
    {
        url: '/Compras/Canasta',
        type: "GET",
        dataType: "JSON",
        data:
        {
            id_canasta: parseInt($("#cmbcesta").val())
        },
        success: function (detallesDeCanasta) {
            var row = "";
            var totalPagar = 0;
            $.each(detallesDeCanasta, function (index, item) {
                var total = parseInt(item.Com_Cantidad) * parseInt(item.Producto.Pdt_precio)
                row += "<tr><td style='text-align:center'>" + item.Com_Cantidad + "</td><td style='text-align:center'>" + item.Producto.Medidas.Mdd_Nombre + "</td><td style='text-align:center'>" + item.Producto.Pdt_nombre + "</td><td style='text-align:center'>" + item.Producto.Pdt_precio + "</td><td style='text-align:center'>" + total + "</td><td style='text-align:center'><input type='button' value='Eliminar' class='btn-danger' onClick='eliminar(" + item.Com_Id + ")' /></td></tr>";
                totalPagar = totalPagar + total;
            })
            $("#txtTotal").html("₡" + totalPagar);
            $("#bodyProductos").html(row);
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}
function eliminar(id)
{
    $.ajax(
    {
        url: '/Compras/eliminarDeCanasta',
        type: "GET",
        dataType: "JSON",
        data:
        {
            id_compra: parseInt(id)
        },
        success: function (resultado) {
            alert(resultado)
            procesoDetalles();
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}