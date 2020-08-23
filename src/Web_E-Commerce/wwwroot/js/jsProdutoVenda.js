
var ObjetoVenda = new Object();

ObjetoVenda.AdicionarCarrinho = function (idProduto) {
    var nome = $("#nome_" + idProduto).val();
    var qtd = $("#qtd_" + idProduto).val();

    $.ajax({
        type: "POST",
        url: "/api/AdicionarProdutosCarrinho",
        dataType: "JSON",
        cache: false,
        async: true,
        data: {
            "id": idProduto, "nome": nome, "qtd": qtd
        },
        success: function (data) {
            if (data.sucesso) {
                //1 alart-success //2 alert-warning // alert-danger
                ObjetoAlerta.AlertaTela(1, "Produto adicionado ao carrinho !");
            }
            else {
                //1 alart-success //2 alert-warning // alert-danger
                ObjetoAlerta.AlertaTela(2, "É necessário efetuar o login !");
            }
        }
    });
}


ObjetoVenda.CarregaProdutos = function () {
    $.ajax({
        type: "GET",
        url: "/api/ListaProdutosComEstoque",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {

            var htmlConteudo = "";

            data.forEach(function (Entities) {
                htmlConteudo += "<div class='col-xs-12 col-sm-4 col-lg-4'>";
                var idNome = "nome_" + Entities.id;
                var idQtd = "qtd_" + Entities.id;

                htmlConteudo += "<label id='" + idNome + "'> Produto: " + Entities.nome + "</label><br/>";
                htmlConteudo += "<label> Valor: " + Entities.valor + " </label><br/>";

                htmlConteudo += "Quantidade : <input type'number' value='1' id='" + idQtd + "'>";

                htmlConteudo += "<input type='button' onclick='ObjetoVenda.AdicionarCarrinho(" + Entities.id + ")' value='Comprar'> </br>";

                htmlConteudo += "</div>";
            });
            $("#DivVendas").html(htmlConteudo);

        }
    });
}

ObjetoVenda.CarregaQtdCarrinho = function () {
    $("#qtdCarrinho").text("(0)");
    $.ajax({
        type: "GET",
        url: "/api/QuantidadeProdutoCarrinho",
        dataType: "JSON",
        cache: false,
        async: true,
        success: function (data) {
            if (data.sucesso) {
                $("#qtdCarrinho").text("("+data.qtd+")");
               
            }
        }
    });

    setTimeout(ObjetoVenda.CarregaQtdCarrinho, 10000);
}

$(function () {
    ObjetoVenda.CarregaProdutos();
    ObjetoVenda.CarregaQtdCarrinho();
});