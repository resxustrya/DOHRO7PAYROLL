//tracking history of the document
var id = 0, salary = 0, half_salary = 0, minutes_late = 0, deduction = 0, net_amount = 0, tax_10 = 0, tax_3 = 0, coop = 0,
    phic = 0, disallowance = 0, gsis = 0, pagibig = 0, excess = 0, total_amount = 0, working_days = 0;
$(document).ready(function () {


    $('.input-daterange input').each(function () {
        $(this).datepicker("clearDates");
    });
    $('#inclusive3').daterangepicker();
    $('#filter_dates').daterangepicker();
    $('#print_pdf').submit(function () {
        $('#upload').button('loading');
        $('#print_individual').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    });

    $('#filter_dates').change(function () {
        var mFrom = $("#filter_dates").val().split("-")[0];
        var mTo = $("#filter_dates").val().split("-")[1];

        var from_year = mFrom.split("/")[2].trimRight();
        from_year = from_year.replace(/\s+/g,'');

        var from_day = mFrom.split("/")[1].trimRight();
        from_day = from_day.replace(/\s+/g, '');

        var from_month = mFrom.split("/")[0].trimRight();
        from_month = from_month.replace(/\s+/g, '');

        var to_year = mTo.split("/")[2].trimRight();
        to_year = to_year.replace(/\s+/g, '');

        var to_day = mTo.split("/")[1].trimRight();
        to_day = to_day.replace(/\s+/g, '');

        var to_month = mTo.split("/")[0].trimRight();
        to_month = to_month.replace(/\s+/g, '');

        mFrom = from_year + "-" + from_month + "-" + from_day;
        mTo =   to_year + "-" + to_month + "-" + to_day;
        
        $.ajax({
            url: "Payroll/GetMins",
            type: 'POST',
            data:
                {
                    "id": id,
                    "from": mFrom,
                    "to": mTo
                },
            success: function (data) {
                minutes_late = data;
                $("#minutes_late").val(minutes_late);
                computeAbsentRate();
            }
        });
    });




    $("a[href='#track']").on('click', function () {
        $('.track_history').html(loadingState);
        var route_no = $(this).data('route');
        var url = $(this).data('link');

        $('#track_route_no').val('Loading...');
        setTimeout(function () {
            $('#track_route_no').val(route_no);
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    $('.track_history').html(data);
                }
            });
        }, 1000);
    });

    $("#coop").change(function () {        
        coop = $(this).val();
        computeTotal();
    });

    $("#phic").change(function () {
        phic = $(this).val();
        computeTotal();
    });

    $("#disallowance").change(function () {
        disallowance = $(this).val();
        computeTotal();
    });

    $("#gsis").change(function () {
        gsis = $(this).val();
        computeTotal();
    });

    $("#pagibig").change(function () {
        pagibig = $(this).val();
        computeTotal();
    });

    $("#excess").change(function () {
        excess = $(this).val();
        computeTotal();
    });

    $("#salary").change(function () {
        salary = $(this).val();
        half_salary = (salary / 2).toFixed(2);
        $("#half_salary").val(half_salary);
        computeAbsentRate();
        netAmount();
        computeTotal();
    });

    $("#minutes_late").change(function () {
        minutes_late = $(this).val();
        computeAbsentRate();
        netAmount();
        computeTotal();

    });

    $("#working_days").change(function () {
        working_days = $(this).val();
        computeAbsentRate();
        netAmount();
        computeTotal();

    });


    $("#button_close").click(function () {
        clearFeld();
    });



    $(".btn_view").click(function () {
        id= $(this).closest('tr').find('td:eq(0)').text();
        var firstname = $(this).closest('tr').find('td:eq(1)').text();
        var surname = $(this).closest('tr').find('td:eq(2)').text();
        var position = $(this).closest('tr').find('td:eq(3)').text();

        salary = $(this).closest('tr').find('td:eq(4)').text();
        half_salary = (salary / 2).toFixed(2);
        minutes_late = $(this).closest('tr').find('td:eq(5)').text();
        working_days = $(this).closest('tr').find('td:eq(13)').text();
        computeAbsentRate();
        netAmount();
        coop = $(this).closest('tr').find('td:eq(6)').text();
        phic= $(this).closest('tr').find('td:eq(7)').text();
        disallowance = $(this).closest('tr').find('td:eq(8)').text();
        gsis = $(this).closest('tr').find('td:eq(9)').text();
        pagibig = $(this).closest('tr').find('td:eq(10)').text();
        excess = $(this).closest('tr').find('td:eq(11)').text();
        var flag = $(this).closest('tr').find('td:eq(12)').text();
        
        $("#type_request").val(flag);
        $("#id").val(id);
        $("#fname").val(firstname);
        $("#lname").val(surname);
        $("#type").val(position);
        $("#working_days").val(working_days);
        $("#salary").val(salary)
        $("#half_salary").val(salary / 2);
        $("#minutes_late").val(minutes_late);
        $("#coop").val(coop);
        $("#phic").val(phic);
        $("#disallowance").val(disallowance);
        $("#gsis").val(gsis);
        $("#pagibig").val(pagibig);
        $("#excess").val(excess);

        computeTotal();
    });
});

function clearFeld() {
    $("#salary").val("");
    $("#half_salary").val("");
    $("#working_days").val("");
    $("#minutes_late").val("");
    $("#deduction").val("");
    $("#net_amount").val("");
    $("#tax_10").val("");
    $("#tax_3").val("");
    $("#coop").val("");
    $("#phic").val("");
    $("#disallowance").val("");
    $("#gsis").val("");
    $("#pagibig").val("");
    $("#excess").val("");
    $("#total_amount").val("");
}

function computeAbsentRate() {
    deduction = (minutes_late * (((salary / working_days) / 8) / 60)).toFixed(2);
    if (isNaN(deduction) == true) {
        deduction = "0";
    }
    $("#deduction").val(deduction);
}

function netAmount() {
    net_amount = 0.00;
    if (working_days != 0 && salary != 0) {
        net_amount = half_salary;
        if (minutes_late > 0) {
            net_amount = (half_salary - deduction).toFixed(2);
        }
    }
    tax_10 = (net_amount * 0.10).toFixed(2);
    tax_3 = (net_amount * 0.03).toFixed(2);
    $("#net_amount").val(net_amount);
    $("#tax_10").val(tax_10);
    $("#tax_3").val(tax_3);
}

function computeTotal() {
    total_amount = (net_amount - tax_10 - tax_3 - coop - disallowance - pagibig - phic - gsis - excess).toFixed(2);
    $("#total_amount").val(total_amount);
}
