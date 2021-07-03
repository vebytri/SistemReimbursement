$(document).ready(function () {
   
        $.ajax({
            url: 'https://localhost:44383/api/Users/'+email,
            type: "GET",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
          
        }).done((result) => {
            $("#email").val(result.email);

        }).fail((error) => {
            Swal.fire({ title: 'Error', 'text': 'Something went wrong', 'type': 'error' });
            Swal.hideLoading();

        })
   
});

function newpassword() {

}