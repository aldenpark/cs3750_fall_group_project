$(document).ready(function () {
    $("button").click(function () {
        event.preventDefault();
        $.ajax({
            url: "api/home", data: { "StockCode": $("#StockObj_StockCode").val() }, dataType: "json", success: function (result) {
                if (result.error) {
                    alert(result.error)
                }
                else {
                    var body = '';

                    body += '<div class="row"><div class="col-md-3">TimeStamp</div>' +
                        '<div class="col-md-1">Open</div>' +
                        '<div class="col-md-1">High</div>' +
                        '<div class="col-md-1">Low</div>' +
                        '<div class="col-md-1">Close</div>' +
                        '<div class="col-md-1">Volume</div></div>';


                    $.each(result.data, function (index, row) {
                        body += '<div class="row"><div class="col-md-3">' + row.timestamp + '</div>' +
                            '<div class="col-md-1">' + row.open + '</div>' +
                            '<div class="col-md-1">' + row.high + '</div>' +
                            '<div class="col-md-1">' + row.low + '</div>' +
                            '<div class="col-md-1">' + row.close + '</div>' +
                            '<div class="col-md-1">' + row.volume + '</div></div>';
                    });

                    $("#json_result").html(body);
                }
            }
        });
    });

});