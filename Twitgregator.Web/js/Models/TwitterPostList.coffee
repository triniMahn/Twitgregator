
class window.TwitterPostList extends Backbone.Model
		
	initialize:->
		

class window.TwitterPostListCollection extends Backbone.Collection
	
	model: TwitterPostList

	url: '/api/home'