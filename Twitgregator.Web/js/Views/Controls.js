(function() {
  var _ref,
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; },
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  window.ControlsView = (function(_super) {
    __extends(ControlsView, _super);

    function ControlsView() {
      this.getTweets = __bind(this.getTweets, this);
      this.render = __bind(this.render, this);      _ref = ControlsView.__super__.constructor.apply(this, arguments);
      return _ref;
    }

    ControlsView.prototype.el = $('#divControls');

    ControlsView.prototype.template = _.template($('#control-template').html());

    ControlsView.prototype.events = {
      'click .btnGetTweets': 'getTweets'
    };

    ControlsView.prototype.render = function() {
      this.$el.html(this.template());
      return this;
    };

    ControlsView.prototype.getTweets = function(e) {
      var handles;

      handles = this.$('.handleInput').val();
      console.log("Handles entered: " + handles);
      return app.controllers.main.navigate('tweets/' + encodeURIComponent(handles), {
        'trigger': true
      });
    };

    return ControlsView;

  })(Backbone.View);

}).call(this);
