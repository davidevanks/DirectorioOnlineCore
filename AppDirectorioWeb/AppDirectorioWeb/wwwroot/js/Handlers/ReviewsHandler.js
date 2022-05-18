$(document).ready(function () {
    loadReviews();
});

function loadReviews() {
  
    var BusinessId = $('#IBusinessId').val();

    $.ajax({
        type: "GET",
        url: '/Negocios/Negocios/GetReviewByBussines/?BusinessId=' + BusinessId,
        success: function (data) {

            $('#revContador').text('Reseñas (' + data.reviewsObj.length+')');
            $('#spanReviewCount').text('(' + data.reviewsObj.length + ' Reseñas)');

            var totalRev = data.reviewsObj.length;
            var sumStars = 0;

            $.each(data.reviewsObj, function (index, val) {

                var starsString = '';
                for (var i = 0; i < this.stars; i++) {
                    starsString += '  <li class="list-inline-item">' +
                        '<i class="fa fa-star" aria-hidden="true" ></i>' +
                        '</li>';
                }

                sumStars += this.stars;

                $("#divContentMainReview").append(
                    ' <div class="media mb-3">' +
                    '<div class="media-img">' +
                    '<img style="width: 50px;" src="/ImagesBusiness/' + this.pictureUser + '" data-src="/ImagesBusiness/' + this.pictureUser + '" class="mr-3 media-object rounded-circle " alt="Image User">' +
                    '   </div>' +
                    '<div class="media-body">' +
                    ' <h5 class="media-heading">' + this.userNameComments + '</h5>' +
                    ' <ul class="list-inline list-inline-rating">' +
                    starsString +
                    '</ul>' +
                    '  <p>' +
                    this.comments +
                    '  </p>' +
                    '</div>' +
                    '</div>' +
                    '  <br/>'
                )
            });

            var promReviewStars = float2int(sumStars / totalRev) ;
            
            for (var i = 0; i < promReviewStars; i++) {
                $("#mainStarsUl").append('  <li class="list-inline-item">' +
                    '<i class="fa fa-star" aria-hidden="true" ></i>' +
                    '</li>');
            }
           
        }
    });

}

function float2int(value) {
    return value | 0;
}

jQuery.extend(jQuery.validator.messages, {
    required: "Este campo es requerido.",
  
});
$('#btnSendReview').click(function () {

    if ($('#reviewForm').valid() == false) {


    } else {
        $('#reviewForm').submit(function (e) {
            e.preventDefault();
        });



    }
    
  
    
});


