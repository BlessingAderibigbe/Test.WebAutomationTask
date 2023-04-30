Feature: ShoppingCart

As a user, I should be able to add four random items to my cart and remove the lowest price item out of the four added

Scenario: User can add items to a cart and remove the lowest price item
	Given I add four random items to my cart
	When I view my cart
	Then I find total 4 items listed in my cart
	When I search for the lowest price item
	And I am able to remove the lowest price item from my cart
	Then I am able to verify 3 items in my cart