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

        var ctxg = document.getElementById("GenderChart");
        var myChart = new Chart(ctxg, {
            type: 'doughnut',
            data: {
                labels: ['MALE', 'FEMALE'],
                datasets: [{
                   
                    data: [malee,femalee],
                    backgroundColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)'
                       
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                aspectRatio: 1,
                cutoutPercentage:80

            }
        });
        //$('#Female').html(femalee);
        //$('#Male').html(malee);   
        //var chart1 = c3.generate({
        //    bindto: '#ct-chart',
        //    data: {
        //        columns: [
        //            ['Male', malee],
        //            ['Female', femalee]
        //        ],

        //        type: 'donut',
        //        tooltip: {
        //            show: true
        //        }
        //    },
        //    donut: {
        //        label: {
        //            show: false
        //        },
        //        title: 'Gender',
        //        width: 18
        //    },

        //    legend: {
        //        hide: true
        //    },
        //    color: {
        //        pattern: [
        //            '#ff4f70',
        //            '#5f76e8'
                    
        //        ]
        //    }
        //});

        //d3.select('#ct-chart .c3-chart-arcs-title').style('font-family', 'Rubik');

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
        var ctxm = document.getElementById("ManagerChart").getContext('2d');

        var ManagerChart = new Chart(ctxm, {
            type: 'bar',
            data: {
                labels: ["Process", "Rejected", "Approved"],
                datasets: [{

                    data: [process, rejected, approved],
                    backgroundColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(75, 192, 192, 1)',

                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(75, 192, 192, 1)',

                    ],
                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,

                            stepSize: 1
                        },
                        gridLines: {
                            display: false
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: false
                        },
                        barPercentage: 0.4
                    }]
                }
            }
        });



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
        var ctx = document.getElementById("FinanceChart").getContext('2d');
  
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ["Process", "Rejected",  "Approved"],
                datasets: [{
                 
                    data: [process1, rejected1, approved1],
                    backgroundColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(75, 192, 192, 1)',
                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(75, 192, 192, 1)',
                       
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                    legend: {
                        display: false
                },
                 responsive: true,
                    maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                        
                            stepSize:1
                        },
                        gridLines: {
                            display: false
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: false
                        },
                        barPercentage: 0.4
                    }]
                }
            }
        });


       



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
        var ctxe = document.getElementById("EmployeeChart").getContext('2d');

        var ManagerChart = new Chart(ctxe, {
            type: 'horizontalBar',
            data: {
                labels: ["Process", "Rejected", "Approved"],
                datasets: [{

                    data: [process2, rejected2, approved2],
                    backgroundColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(75, 192, 192, 1)',

                    ],
                    borderColor: [
                        'rgba(54, 162, 235, 1)',
                        'rgba(255,99,132,1)',
                        'rgba(75, 192, 192, 1)',

                    ],
                    borderWidth: 1
                }]
            },
            options: {

                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,

                            stepSize: 1
                        },
                        barPercentage: 0.4,
                        gridLines: {
                            display: false
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            beginAtZero: true,

                            stepSize: 1
                        },
                        gridLines: {
                            display: false
                        },
                       
                    }]
                }
            }
        });



    }).fail((error) => {


    })
    
 });

