# NET6_Assignment

## Task 1 (C#)

In the first task I implemented the solution in 2 different projects.
- CashMachineService (I went for a Class Library)
- CashMahcineService.Tests

For the tests I used XUnit and ran the the test suite with the following command from the cash-machine-helmes directory

```
dotnet test
```

Currently my solution for withdrawing banknotes from the cash machine is how many of the biggest banknotes (100) I can use, then move on to 50 ... . I chose this solution as it is relatively simple and it resembles real-life to some point that ATM give you the biggest notes possible at first. While this would be acceptable for basic use cases, there are use cases, where other solutions like
- Recursively checking different combinations and orders of banknotes to find a solution that the user can withdraw.
- Other DFS/BFS solutions that can find an optimal solution based on rules. 

## Task 2 (SQL)

For testing I used a PostgreSQL database. The queries might differ a bit from normal since I had to reference the tables as public. I added all of the commands that I used to, create the database, data insertion and test the query for the task (query.sql).

The end query is 

```sql
DELETE FROM public.shipments using public.orders, public.customers
WHERE shipment_date < '2021-01-01'
AND public.shipments.order_id = public.orders.order_id
AND public.orders.customer_id = public.customers.customer_id
AND public.customers.customer_name = 'John Doe'
```
Translated it to the standard SQL would be
```sql
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

```

It assumes that shipments made on 2021-01-01 will not be deleted.

## Task 3 (Blazor/Razor)

For this I generated a sample Blazor project

```
dotnet new blazorwasm -o
```

Removed the Bootstrap from the project, then added MudBlazor UI library. After which I used it to develop the Razor component that has alarms (alerts in this case) on a digital clock.

Running the app with the following command
```
dotnet watch run
```

I tried to take into how it would look on other screens other than a usual monitor like a phone or a tablet aswell (Used Chrome Device toolbar).

Future plans would be to add a alert sound when the popup alert comes up. Currently by closing the alert, the alarm gets deleted aswell.

NB: The MudBlazor MudTimePicker component seems to lag behind when selecting time, but I chose to not investigate it as it would take too much of the assignment's time.