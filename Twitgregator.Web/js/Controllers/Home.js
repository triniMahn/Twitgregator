(function() {
  var HomeController, Vent, _ref, _ref1,
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  Vent = (function(_super) {
    __extends(Vent, _super);

    function Vent() {
      _ref = Vent.__super__.constructor.apply(this, arguments);
      return _ref;
    }

    return Vent;

  })(Backbone.Events);

  window.Vent = Vent;

  HomeController = (function(_super) {
    __extends(HomeController, _super);

    function HomeController() {
      _ref1 = HomeController.__super__.constructor.apply(this, arguments);
      return _ref1;
    }

    HomeController.prototype.routes = {
      '': 'getControls',
      'home': 'getControls',
      'tweets/:names': 'getTweets'
    };

    HomeController.prototype.getControls = function() {
      var cv;

      cv = new ControlsView();
      return cv.render();
    };

    HomeController.prototype.getTweets = function(names) {
      var coll, td,
        _this = this;

      coll = new TwitterPostListCollection();
      td = new TweetDataListView({
        collection: coll
      });
      return coll.fetch({
        data: {
          screenNames: names
        },
        error: function(collection, response) {
          return alert('error');
        }
      });
    };

    return HomeController;

  })(Backbone.Router);

  window.SpinOpts = {
    lines: 13,
    length: 20,
    width: 10,
    radius: 30,
    corners: 1,
    rotate: 0,
    direction: 1,
    color: '#000',
    speed: 1,
    trail: 60,
    shadow: false,
    hwaccel: false,
    className: 'LoadingSpinner',
    zIndex: 2e9,
    top: 'auto',
    left: 'auto'
  };

  $(document).ready(function() {
    var divLoading, spinner;

    divLoading = $('#divLoadingSpinner');
    spinner = new Spinner(SpinOpts).spin(divLoading);
    divLoading.ajaxStart(function() {
      return divLoading.fadeIn();
    });
    divLoading.ajaxComplete(function() {
      return divLoading.fadeOut();
    });
    app.controllers.main = new HomeController();
    return Backbone.history.start();
  });

}).call(this);
