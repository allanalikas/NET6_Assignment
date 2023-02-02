CREATE TABLE public.customers
(
    customer_id integer NOT NULL,
	customer_name char(50) NOT NULL,
    PRIMARY KEY (customer_id)
);
	
CREATE TABLE public.orders
(
    order_id integer NOT NULL,
	customer_id integer NOT NULL references customers(customer_id),
	order_date date NOT NULL,
    PRIMARY KEY (order_id)
);

CREATE TABLE public.shipments
(
    shipment_id integer NOT NULL,
	order_id integer NOT NULL references orders(order_id),
	shipment_date date NOT NULL,
    PRIMARY KEY (shipment_id)
);
	
DROP TABLE IF EXISTS public.customers, public.orders, public.shipments;
	
DELETE FROM public.shipments;
DELETE FROM public.orders;
DELETE FROM public.customers;

INSERT INTO public.customers VALUES(1,'Morgana');
INSERT INTO public.customers VALUES(2,'John Doe');

INSERT INTO public.orders VALUES(1, 1, current_date);
INSERT INTO public.orders VALUES(2, 2, current_date);

INSERT INTO public.shipments VALUES(1, 1, current_date);
INSERT INTO public.shipments VALUES(2, 1, '2020-01-01');
INSERT INTO public.shipments VALUES(3, 1, '2024-01-01');
INSERT INTO public.shipments VALUES(4, 2, current_date);
INSERT INTO public.shipments VALUES(5, 2, '2020-01-01');
INSERT INTO public.shipments VALUES(6, 2, '2024-01-01');

SELECT * FROM public.customers
SELECT * FROM public.orders
SELECT * FROM public.shipments

DELETE FROM public.shipments using public.orders, public.customers
WHERE shipment_date < '2021-01-01'
AND public.shipments.order_id = public.orders.order_id
AND public.orders.customer_id = public.customers.customer_id
AND public.customers.customer_name = 'John Doe'

DELETE FROM shipments 
WHERE shipment_date < '2021-01-01'
AND shipments.order_id IN
	(
	SELECT orders.order_id FROM orders
	WHERE orders.customer_id IN
		(
			SELECT customers.customer_id
			FROM customers
			WHERE customers.customer_name = 'John Doe'
		)
	)



