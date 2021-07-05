$(document).ready(function () {
    var malee = 0;
    var femalee = 0;
    $.ajax({
        url: 'https://localhost:44383/api/users/',
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        //console.log(result);
        //console.log(result[0].gender);
        for (var i = 0; i < result.length; i++) {

            if (result[i].gender == "Male") {
                //console.log("Male");
                //var Male = "Male";
                malee = malee + 1;

            }
            else if (result[i].gender == "Female") {
                //console.log("Female");
                //var Female = "Female";  
                femalee++;
            }
        }
        $('#Female').html(femalee);
        $('#Male').html(malee);   
        var chart1 = c3.generate({
            bindto: '#ct-chart',
            data: {
                columns: [
                    ['Male', malee],
                    ['Female', femalee]
                ],

                type: 'donut',
                tooltip: {
                    show: true
                }
            },
            donut: {
                label: {
                    show: false
                },
                title: 'Gender',
                width: 18
            },

            legend: {
                hide: true
            },
            color: {
                pattern: [
                    '#ff4f70',
                    '#5f76e8'
                    
                ]
            }
        });

        d3.select('#ct-chart .c3-chart-arcs-title').style('font-family', 'Rubik');

    }).fail((error) => {


    })


    var approved = 0;
    var rejected = 0;
    var process = 0;

    $.ajax({
        url: 'https://localhost:44383/api/reimbursements/',
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        //console.log(result);
        //console.log(result[0].managerApprovalStatus);
        for (var i = 0; i < result.length; i++) {

            if (result[i].managerApprovalStatus == 0 ) {
                //console.log("Male");
                //var Male = "Male";
              process++;

            }
          else  if (result[i].managerApprovalStatus == 1) {

                approved++;
            }
            else if (result[i].managerApprovalStatus == 2) {

                rejected++;

            }
        }
       
        
        // income
        // ============================================================== 
        var data = {
            labels: ['Approved', 'Rejected', 'Process'],
            series: [
                [approved, rejected, process]
            ]
        };

        var options = {
            axisX: {
                showGrid: false
            },
            seriesBarDistance: 1,
            chartPadding: {
                top: 15,
                right: 15,
                bottom: 5,
                left: 0
            },
            plugins: [
                Chartist.plugins.tooltip()
            ],
            width: '100%'
        };

        var responsiveOptions = [
            ['screen and (max-width: 640px)', {
                seriesBarDistance: 5,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value[0];
                    }
                }
            }]
        ];
        new Chartist.Bar('#manager', data, options, responsiveOptions);


    }).fail((error) => {


    })

    var approved1 = 0;
    var rejected1 = 0;
    var process1 = 0;

    $.ajax({
        url: 'https://localhost:44383/api/reimbursements/',
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        //console.log(result);
        //console.log(result[0].managerApprovalStatus);
        for (var i = 0; i < result.length; i++) {

            if (result[i].financeApprovalStatus == 0) {
            
                process1++;

            }
            else if (result[i].financeApprovalStatus == 1) {

                approved1++;
            }
            else if (result[i].financeApprovalStatus == 2) {

                rejected1++;

            }
        }


        // income
        // ============================================================== 
        var data = {
            labels: ['Approved', 'Rejected', 'Process'],
            series: [
                [approved1, rejected1, process1]
            ]
        };

        var options = {
            axisX: {
                showGrid: false
            },
            seriesBarDistance: 1,
            chartPadding: {
                top: 15,
                right: 15,
                bottom: 5,
                left: 0
            },
            plugins: [
                Chartist.plugins.tooltip()
            ],
            width: '100%'
        };

        var responsiveOptions = [
            ['screen and (max-width: 640px)', {
                seriesBarDistance: 5,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value[0];
                    }
                }
            }]
        ];
        new Chartist.Bar('#finance', data, options, responsiveOptions);


    }).fail((error) => {


    })




    var approved2 = 0;
    var rejected2 = 0;
    var process2 = 0;
    var nik3 = $('#nik3').val();
    $.ajax({
        url: 'https://localhost:44383/api/reimbursements/getallbynik/'+nik3,
        type: "GET",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).done((result) => {
        //console.log(result);
        //console.log(result[0].managerApprovalStatus);
        for (var i = 0; i < result.length; i++) {

            if (result[i].financeApprovalStatus == 0) {

                process2++;

            }
            else if (result[i].financeApprovalStatus == 1) {

                approved2++;
            }
            else if (result[i].financeApprovalStatus == 2) {

                rejected2++;

            }
        }
        // income
        // ============================================================== 
        var data = {
            labels: ['Approved', 'Rejected', 'Process'],
            series: [
                [approved2, rejected2, process2]
            ]
        };
        var options = {
            axisX: {
                showGrid: false
            },
            seriesBarDistance: 10,
            reverseData: true,
            horizontalBars: true,
            axisY: {
                offset: 70
            },
            plugins: [
                Chartist.plugins.tooltip()
            ],
            width: '100%'
        };

        var responsiveOptions = [
            ['screen and (max-width: 640px)', {
                seriesBarDistance: 5,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value[0];
                    }
                }
            }]
        ];
        new Chartist.Bar('#employee', data, options, responsiveOptions);


    }).fail((error) => {


    })
 });

