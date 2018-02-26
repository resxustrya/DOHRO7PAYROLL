//tracking history of the document
var id = 0, salary = 0, half_salary = 0, minutes_late = 0, deduction = 0, net_amount = 0, tax_10 = 0, tax_3 = 0, tax_2 = 0, coop = 0,
    phic = 0, disallowance = 0, gsis = 0, pagibig = 0, excess = 0, total_amount = 0, working_days = 0, am_in = 0, am_out = 0, pm_in = 0, pm_out = 0,
    adjustment = 0, array_date = [], no_days_absent=0,count=0;
$(document).ready(function () {
  
    $('.input-data').keypress(function (event) {
        return isNumber(event, this)
    });

    $('.input-pin').keypress(function (event) {
        return isPIN(event, this)
    });

    $('.input-daterange input').each(function () {
        $(this).datepicker("clearDates");
    });

  

    $("#month").change(function () {

        var chosen_month = $(this).val();
        var chosen_year = $("#year").val();
        if (+chosen_month <= 9) chosen_month = "0" + chosen_month;
        var chosen_options = $("#month_range").val();
        var from_date = "";
        var to_date = "";
        var myNode = document.getElementById("date-list");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
        $(".loader_ajax").removeClass("hidden");
        $(".modal-body :input").attr("disabled", true);
        switch (chosen_options) {
            case "1":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-01",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month + "-15"
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                         minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                       no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                       $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                      //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                        
                    }
                });
                $("#half_month_text").text("Half Month");
                from_date = chosen_month + "/01/" + chosen_year;
                to_date = chosen_month + "/15/" + chosen_year;
                half_salary = (salary / 2).toFixed(2);

                break;
            case "2":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-16",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month+ "-" + daysInMonth(parseInt(chosen_month), chosen_year)
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Half Month");
                from_date = chosen_month + "/16/" + chosen_year;
                to_date = chosen_month + "/" + daysInMonth(parseInt(chosen_month), chosen_year) + "/" + chosen_year;
                half_salary = (salary / 2).toFixed(2);
                break;
            case "3":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-01",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month +"-"+ daysInMonth(parseInt(chosen_month), chosen_year)
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Whole Month");
                from_date = chosen_month + "/01/" + chosen_year;
                to_date = chosen_month + "/" + daysInMonth(parseInt(chosen_month), chosen_year) + "/" + chosen_year;
                half_salary = salary;
                break;
        }
        $("#month_range_value").val(from_date + " " + to_date);
        if (+salary != 0) {
            $("#half_salary").val(formatComma(half_salary));
        }
    });

    $("#year").change(function () {
        
        var chosen_year = $(this).val();
        var chosen_options = $("#month_range").val();
        var chosen_month = $("#month").val();
        if (+chosen_month <= 9) chosen_month = "0" + chosen_month;
        var from_date = "";
        var to_date = "";
        var myNode = document.getElementById("date-list");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
        $(".loader_ajax").removeClass("hidden");
        $(".modal-body :input").attr("disabled", true);
        switch (chosen_options) {
            case "1":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-01",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month + "-15"
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Half Month");
                from_date = chosen_month + "/01/" + chosen_year;
                to_date = chosen_month + "/15/" + chosen_year;
                half_salary = (salary / 2).toFixed(2);
                break;
            case "2":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-16",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month+ "-" + daysInMonth(parseInt(chosen_month), chosen_year)
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Half Month");
                from_date = chosen_month + "/16/" + chosen_year;
                to_date = chosen_month + "/" + daysInMonth(parseInt(chosen_month), chosen_year) + "/" + chosen_year;
                half_salary = (salary / 2).toFixed(2);
                break;
            case "3":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-01",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month +"-"+ daysInMonth(parseInt(chosen_month), chosen_year)
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                      minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                       no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                       $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                      //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Whole Month");
                from_date = chosen_month + "/01/" + chosen_year;
                to_date = chosen_month + "/" + daysInMonth(parseInt(chosen_month), chosen_year) + "/" + chosen_year;
                half_salary = salary;
                break;
        }
        $("#month_range_value").val(from_date + " " + to_date);
        if (+salary != 0) {
            $("#half_salary").val(formatComma(half_salary));
        }
    });

    $("#month_range").change(function () {
        var chosen_options = $(this).val();
        var chosen_year = $("#year").val();
        var chosen_month = $("#month").val();
        if (+chosen_month <= 9) chosen_month = "0" + chosen_month;
        var from_date = "";
        var to_date = "";
        var myNode = document.getElementById("date-list");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
        $(".loader_ajax").removeClass("hidden");
        $(".modal-body :input").attr("disabled", true);
        switch (chosen_options) {
            case "1":
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-01",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month + "-15"
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Half Month");
                from_date = chosen_month + "/01/" + chosen_year;
                to_date = chosen_month + "/15/" + chosen_year;
                half_salary = (salary / 2).toFixed(2);
                break;
            case "2":
                $("#pagibig").val("0.00");
                $("#coop").val("0.00");
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-16",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month + "-" + daysInMonth(parseInt(chosen_month), chosen_year)
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Half Month");
                from_date = chosen_month + "/16/" + chosen_year;
                to_date = chosen_month + "/" + daysInMonth(parseInt(chosen_month), chosen_year) + "/" + chosen_year;
                half_salary = (salary / 2).toFixed(2);
                break;
            case "3":
                $("#pagibig").val("0.00");
                $("#coop").val("0.00");
                $.ajax({
                    url: "../Payroll/GetMins",
                    type: 'POST',
                    data:
                        {
                            "id": id,
                            "from": chosen_year + "-" + chosen_month + "-01",
                            "am_in": "08:00:00",
                            "am_out": "12:00:00",
                            "pm_in": "13:00:00",
                            "pm_out": "17:00:00",
                            "to": chosen_year + "-" + chosen_month+ "-" + daysInMonth(parseInt(chosen_month), chosen_year)
                        },
                    success: function (data) {
                        array_date = [];
                        $("#absent_date_list").val("");
                        minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                        no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                        $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                        //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
                    }
                });
                $("#half_month_text").text("Whole Month");
                from_date = chosen_month + "/01/" + chosen_year;
                to_date = chosen_month + "/" + daysInMonth(parseInt(chosen_month), chosen_year) + "/" + chosen_year;
                half_salary = salary;
                break;
        }
        $("#month_range_value").val(from_date + " " + to_date);
        if (+salary != 0) {
            $("#half_salary").val(formatComma(half_salary));
        }
    });

  
    $('#absent_date').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true
    },
    function (start, end, label) {
        var item = $('#absent_date').val();
        if (array_date.indexOf(item) === -1) {
            var date_item = "<tr id = '" + count + "'><td>";
            date_item += $('#absent_date').val();
            date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + count + "'></span></td>";
            $("#date-list").append(date_item);
            count++;
            array_date.push(item);
            $("#absent_date_list").val(array_date);
            $('#absent_date').val("");
            no_days_absent = count;
            Compute();
        } else {
            alert("Date already exists");
        }
    });
   
    $('body').on('click', '.remove-data', function () {
        var value =  $(this).closest('tr').find('td:eq(0)').text();
        var id = $(this).data('id');
        $('#' + id).remove();
        removeItem(value);
        count--;
        $("#absent_date_list").val(array_date);
        no_days_absent = count;
        Compute();
    });
   
    $('#inclusive3').daterangepicker();
    $('#filter_dates').daterangepicker();
    $('#pay_dates').daterangepicker();
    $('#search').daterangepicker();
    $('#print_pdf').submit(function () {
        $('#upload').button('loading');
        $('#print_individual').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    });
    $(".btn_payroll_print").click(function (e) {
        $('#payslip_print').modal('show');
    });
    
    $(".btn_print").click(function () {
        var id = $(this).closest('tr').find('td:eq(0)').text();
        var start_date = $(this).closest('tr').find('td:eq(3)').text();
        var end_date = $(this).closest('tr').find('td:eq(4)').text();
        $('#payslip_print').modal('show');
        $("#update_payroll_container").addClass('hidden');
        $.ajax({
            url: "../Payroll/CreateJoPayslip",
            type: 'POST',
            data:
                {
                    "id": id,
                    "start_date": start_date,
                    "end_date": end_date
                },
            success: function (data) {
                $('#payslip_print').modal('hide');
                $("#payroll_list_msg_container").removeClass('hidden');
                $("#payroll_list_msg").text(data);
            }
        });
    });
    $(".btn_add").click(function () {
        id = $(this).data('id');
        $(".modal-body #id").val(id);
        salary = parseFloat($("#salary_original").val()).toFixed(2);
        var orig_salary = $("#salary_original").val();
        if (+orig_salary >= 17000) {
            //   $(".tax_2_container").addClass("hidden");
            //  $(".tax_10_container").removeClass("hidden");
        } else {
            // $(".tax_10_container").addClass("hidden");
            // $(".tax_2_container").removeClass("hidden");
        }
        $("#salary").val(formatComma(salary));
        adjustment = parseFloat("0").toFixed(2);
        $("#adjustment").val(formatComma(adjustment));
        half_salary = (+salary / 2).toFixed(2);
        $("#half_salary").val(formatComma(half_salary));
        minutes_late = 0;
        $("#minutes_late").val(minutes_late);
        coop = parseFloat($("#coop_original").val()).toFixed(2);
        $("#coop").val(formatComma(coop));
        disallowance = parseFloat($("#disallowance_original").val()).toFixed(2);
        $("#disallowance").val(formatComma(disallowance));
        pagibig = parseFloat($("#pagibig_original").val()).toFixed(2);
        $("#pagibig").val(formatComma(pagibig));
        phic = parseFloat($("#phic_original").val()).toFixed(2);
        $("#phic").val(formatComma(phic));
        gsis = parseFloat($("#gsis_original").val()).toFixed(2);
        $("#gsis").val(formatComma(gsis));
        excess = parseFloat($("#excess_original").val()).toFixed(2);
        $("#excess").val(formatComma(excess));
        var start_date = "01/01/2017";
        var end_date = "01/15/2017";
        $("#month_range_value").val(start_date + " " + end_date);
        $(".loader_ajax").removeClass("hidden");
        $(".modal-body :input").attr("disabled", true);
        $.ajax({
            url: "../Payroll/GetMins",
            type: 'POST',
            data:
                {
                    "id": id,
                    "from": "2017-01-01",
                    "am_in": "08:00:00",
                    "am_out": "12:00:00",
                    "pm_in": "13:00:00",
                    "pm_out": "17:00:00",
                    "to": "2017-01-15"
                },
            success: function (data) {
                array_date = [];
                $("#absent_date_list").val("");
             minutes_late = data.split(" ")[0];
                        working_days = data.split(" ")[1];
                       no_days_absent = data.split(" ")[2];

                        count = 0;
                        for (var z = 0; z < no_days_absent.split("*").length; z++) {
                            var item = no_days_absent.split("*")[count];
                            if (item !== "") {
                                var date_item = "<tr id = '" + z + "'><td>";
                                date_item += item;
                                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                                $("#date-list").append(date_item);
                                array_date.push(item);
                                $("#absent_date_list").val(array_date);
                                $('#absent_date').val("");
                                count++;
                            }
                        }

                       $("#minutes_late").val(minutes_late);
                        $("#working_days").val(working_days);
                      //  $("#no_days_absent").val(no_days_absent);
                        Compute();
                        $(".loader_ajax").addClass("hidden");
                        $(".modal-body :input").attr("disabled", false);
            }
        });
       
    });
   

    $("#adjustment").change(function () {
        adjustment = $(this).val();
        if (adjustment == '') {
            adjustment = "0.00";
        }
        adjustment = parseFloat(adjustment.replace(/,/g, '')).toFixed(2);
        $("#adjustment").val(formatComma(adjustment))
        Compute();
    });

    $("#coop").change(function () {        
        coop = $(this).val();
        if (coop == '') {
            coop = "0.00";
        }
        coop = parseFloat(coop.replace(/,/g, '')).toFixed(2);
        $("#coop").val(formatComma(coop))
        Compute();
    });

    $("#phic").change(function () {
        phic = $(this).val();
        if (phic == '') {
            phic = "0.00";
        }
        phic = parseFloat(phic.replace(/,/g, '')).toFixed(2);
        $("#phic").val(formatComma(phic))
        Compute();
    });

    $("#disallowance").change(function () {
        disallowance = $(this).val();
        if (disallowance == '') {
            disallowance = "0.00";
        }
        disallowance = parseFloat(disallowance.replace(/,/g, '')).toFixed(2);
        $("#disallowance").val(formatComma(disallowance))
        Compute();
    });

    $("#gsis").change(function () {
        gsis = $(this).val();
        if (gsis == '') {
            gsis = "0.00";
        }
        gsis = parseFloat(gsis.replace(/,/g, '')).toFixed(2);
        $("#gsis").val(formatComma(gsis))
        Compute();
    });

    $("#pagibig").change(function () {
        pagibig = $(this).val();
        if (pagibig == '') {
            pagibig = "0.00";
        }
        pagibig = parseFloat(pagibig.replace(/,/g, '')).toFixed(2);
        $("#pagibig").val(formatComma(pagibig))
        Compute();
    });

    $("#excess").change(function () {
        excess = $(this).val();
        if (excess == '') {
            excess = "0.00";
        }
        excess = parseFloat(excess.replace(/,/g, '')).toFixed(2);
        $("#excess").val(formatComma(excess))
        Compute();
    });

    $("#salary").change(function () {
        salary = $(this).val();
        if (salary == '') {
            salary = "0.00";
        }
        salary = parseFloat(salary.replace(/,/g, '')).toFixed(2);
        half_salary = (salary / 2).toFixed(2);
        var id = $("#month_range").val();
        if (id == "3") {  
            half_salary = salary;
        } 
        
        $("#salary").val(formatComma(salary))
        $("#half_salary").val(formatComma(half_salary));
    
    });

    $("#minutes_late").change(function () {
        minutes_late = $(this).val();
        Compute();

    });

    $("#working_days").change(function () {
        working_days = $(this).val();
       
    });


    $("#button_close").click(function () {
        clearFeld();
    });

    $('#modal').on('hidden.bs.modal', function () {
        clearFeld();

    })
    $(".remit_btn_add").click(function () {
        $("#remit_empID").attr("readonly", false);
        $("#remit_submit").val("0");
    });

    $(".btn_edit_remit").click(function () {
        var id = $(this).closest('tr').find('td:eq(0)').text();
        var max = $(this).closest('tr').find('td:eq(1)').text();
        var count = $(this).closest('tr').find('td:eq(2)').text();
        var amount = $(this).closest('tr').find('td:eq(3)').text();
        $("#remit_empID").attr("readonly", true);
        $("#remit_empID").val(id);
        $("#remit_maxCount").val(max);
        $("#remit_count").val(count);
        $("#remit_amount").val(amount);
        $("#remit_submit").val("1");

    });

    $(".btn_edit").click(function () {
        id= $(this).closest('tr').find('td:eq(0)').text();
        var firstname = $(this).closest('tr').find('td:eq(1)').text();
        var surname = $(this).closest('tr').find('td:eq(2)').text();

        var start_date = $(this).closest('tr').find('td:eq(3)').text();
        var end_date = $(this).closest('tr').find('td:eq(4)').text();
        $("#month").val(parseInt(start_date.split("/")[0]));
        var chosen_month = parseInt(start_date.split("/")[0]);
        salary = $(this).closest('tr').find('td:eq(5)').text();

        var orig_salary = salary.replace(",", "");
        if (+orig_salary >= 17000) {
            //   $(".tax_2_container").addClass("hidden");
            //  $(".tax_10_container").removeClass("hidden");
        } else {
            // $(".tax_10_container").addClass("hidden");
            // $(".tax_2_container").removeClass("hidden");
        }
        half_salary = (salary / 2).toFixed(2);
        if (parseInt(start_date.split("/")[1]) == 1 && parseInt(end_date.split("/")[1]) == 15) {
            $("#half_month_text").text("Half Month");
            $("#month_range").val("1");
        } else if (parseInt(start_date.split("/")[1]) == 16) {
            $("#half_month_text").text("Half Month");
            $("#month_range").val("2");
        } else {
            $("#half_month_text").text("Whole Month");
            $("#month_range").val("3");
            half_salary = salary;
        }

        $("#month_range_value").val(start_date + " " + end_date);

        //var position = $(this).closest('tr').find('td:eq(3)').text();
       
        minutes_late = $(this).closest('tr').find('td:eq(6)').text();
        working_days = $(this).closest('tr').find('td:eq(13)').text();
        no_days_absent = $(this).closest('tr').find('td:eq(14)').text();
        count = 0;
        for (var z = 0; z < no_days_absent.split(",").length; z++) {
            var item = no_days_absent.split(",")[count];
            if (item !== "") {
                var date_item = "<tr id = '" + z + "'><td>";
                date_item += item;
                date_item += "</td><td><span class = 'glyphicon glyphicon-remove remove-data' style = 'color:red;cursor:pointer;' data-id='" + z + "'></span></td>";
                $("#date-list").append(date_item);
                array_date.push(item);
                $("#absent_date_list").val(array_date);
                $('#absent_date').val("");
                count++;
            }   
        }
        no_days_absent = count;
        coop = $(this).closest('tr').find('td:eq(7)').text();
        phic = $(this).closest('tr').find('td:eq(8)').text();
        disallowance = $(this).closest('tr').find('td:eq(9)').text();
        gsis = $(this).closest('tr').find('td:eq(10)').text();
        pagibig = $(this).closest('tr').find('td:eq(11)').text();
        excess = $(this).closest('tr').find('td:eq(12)').text();
        var remarks = $(this).closest('tr').find('td:eq(15)').text();
        var payroll_id = $(this).closest('tr').find('td:eq(16)').text();
        adjustment = $(this).closest('tr').find('td:eq(17)').text();

       
        $("#id").val(id);
        $("#payroll_id").val(payroll_id);
        $("#remarks").val(remarks);
        $("#fname").val(firstname);
        $("#lname").val(surname);        
        $("#working_days").val(working_days);
        $("#no_days_absent").val(no_days_absent);
        $("#adjustment").val(adjustment);
        $("#salary").val(formatComma(salary))
        $("#half_salary").val(formatComma(half_salary));
        $("#minutes_late").val(minutes_late);
        $("#coop").val(formatComma(coop));
        $("#phic").val(formatComma(phic));
        $("#disallowance").val(formatComma(disallowance));
        $("#gsis").val(formatComma(gsis));
        $("#pagibig").val(formatComma(pagibig));
        $("#excess").val(formatComma(excess));
        Compute();

        var request_type = $(this).val();
        switch(request_type){
            case "update":
                $("#btn_save").html("Update");
                break;
            case "create":
                $("#btn_save").html("Save");
                break;
            case "print":
                $("#modal_date_range_container").css("visibility","");
                $("#btn_save").html("Print");
        }
    });
});



function clearFeld() {
    $("#month").val("1");
    $("#month_range").val("1");
    $("#year").val("2017");
    $("#type_request").val("0");
    $("#working_days").val("");
    $("#minutes_late").val("");
    $("#absent_date").val("");
    $("#date-list").empty();
    $("#absent_date_list").val("");
    $("#adjustment").val("");
    $("#deduction").val("");
    $("#net_amount").val("");
    $("#tax_10").val("");
    $("#tax_3").val("");
    $("#tax_2").val("");
    $("#total_amount").val("");
    $("#payroll_id").val("");
    $("#remarks").val("");

    id = 0;
    salary = 0;
    half_salary = 0;
    minutes_late = 0;
    deduction = 0;
    net_amount = 0;
    tax_10 = 0;
    tax_3 = 0;
    tax_2 = 0;
    coop = 0;
    phic = 0;
    disallowance = 0;
    gsis = 0;
    pagibig = 0;
    excess = 0;
    total_amount = 0;
    working_days = 0;
    am_in = 0;
    am_out = 0;
    pm_in = 0;
    pm_out = 0;
    adjustment = 0;
    no_days_absent = 0;
    count = 0
    array_date = [];
}
function Compute() {
    if (count > 0) {
        deduction = ((+minutes_late + (480 * +count)) * (((salary / working_days) / 8) / 60)).toFixed(2);
    } else {
        deduction = (minutes_late * (((salary / working_days) / 8) / 60)).toFixed(2);   
    }
    if (isNaN(deduction) || !isFinite(deduction)) {
        deduction = "0";
    }
    $("#deduction").val(formatComma(deduction));

    net_amount = "0.00";
    if (working_days != 0 && salary != 0) {
        net_amount = (parseFloat(half_salary)).toFixed(2);
        if (deduction > 0) {
            net_amount = (parseFloat(half_salary) - parseFloat(deduction)).toFixed(2);
        }
    }
    net_amount = (parseFloat(net_amount) + parseFloat(adjustment)).toFixed(2);
    tax_10 = (net_amount * 0.10).toFixed(2);
    tax_3 = (net_amount * 0.03).toFixed(2);
    tax_2 = (net_amount * 0.02).toFixed(2);
    if (+salary >= 17000) {
        tax_2 = (0).toFixed(2);
    } else {
        tax_10 = (0).toFixed(2);
    }
    $("#net_amount").val(formatComma(net_amount));
    $("#tax_10").val(formatComma(tax_10));
    $("#tax_3").val(formatComma(tax_3));
    $("#tax_2").val(formatComma(tax_2));

    total_amount = (net_amount - tax_10 - tax_3-tax_2- coop - disallowance - pagibig - phic - gsis - excess).toFixed(2);
    $("#total_amount").val(formatComma(total_amount));

}

function formatComma(number) {
    number = number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return number;
}
function isNumber(evt, element) {

    var charCode = (evt.which) ? evt.which : event.keyCode

    if (
        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function isPIN(evt, element) {
    var inputs = $(element).val();
    if (inputs.length <= 3) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (
            (charCode < 48 || charCode > 57)) {
            return false;
        }
        else {
            return true;
        }
    } else {
        return false;
    }  
}

function daysInMonth(month,year) {
    return new Date(year, month, 0).getDate();
}
function removeItem(name){
    for (var i = array_date.length - 1; i >= 0; i--) {
        if (array_date[i] === name) {
            array_date.splice(i, 1);
            // break;       //<-- Uncomment  if only the first term has to be removed
        }
    }
}
