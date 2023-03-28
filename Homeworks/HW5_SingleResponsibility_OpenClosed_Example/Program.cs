Product product1 = new Clothing(1, "Hat", 30);
Product product2 = new Electronic(1, "Phone", 2000, 2);

GoldCard goldCard = new GoldCard();
ShoppingCart cart = new ShoppingCart(goldCard);
cart.AddProductToCart(product1);
cart.AddProductToCart(product2);
cart.DisplayCartSummary();
