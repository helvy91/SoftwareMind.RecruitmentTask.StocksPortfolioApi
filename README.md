StocksPortfolioAPI

Welcome to StocksPortfolioAPI, a project developed for the recruitment task for Softwareminds. This API is designed to manage stock portfolios, providing functionality to retrieve portfolios by ID, calculate the total value of portfolio stocks in a specified currency, and perform soft deletion of portfolios.
Functionality

The main functionalities of the StocksPortfolioAPI include:

    Get Portfolio by ID: Retrieve portfolio information by providing its unique identifier.
    Get Portfolio Stocks Total Value in Specified Currency: Calculate the total value of stocks within a portfolio based on the specified currency.
    Soft Delete Portfolio: Perform a soft delete operation on a portfolio, marking it as inactive without removing it permanently.

Changes Made
Project Structure

    The project structure has been adjusted to adhere to the Onion Architecture pattern, ensuring better separation of concerns and modularity.

Class Restructuring

    Classes have been refactored and split to improve responsibility division, making the codebase more maintainable and easier to comprehend.

Implementation of Caching Mechanism

    A caching mechanism has been implemented to improve performance by storing frequently accessed data.

Addition of Unit Tests

    Unit tests have been added to ensure the reliability and correctness of the implemented functionalities.

Controller Implementation and Routing

    The controller implementation and routing have been updated to align with the revised project structure and improve API endpoint organization.
