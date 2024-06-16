Feature: CheckForInvalidInfrastructre
	As an MSFS user and AI Air Trafic Control User
	I Want Air Trafic Control to know what Infrastructure is invalid for my aircraft type
	So that I don't receive taxi requests via invalid areas similarly to the real world, unless data is missing

Scenario Outline: Check For Invalid Taxiways only when valid data is available
	Given that I am an '<aircraft>' at '<icao>' and '<mass>'
	When I ask for taxi
	Then If data is valid I want to know what '<taxiways are invalid>'
Scenarios: 
 | scenario                       | aircraft | icao | mass     | taxiways are invalid        |
 | Valid data                     | A320     | EGNJ | 44000kgs | TAXI_B,TAXI_E,TAXI_G,TAXI_H |
 | Valid data no invalid Taxiways | C152     | EGNJ | 2499kgs  |                             |
 | Valid data invalid Taxiways    | C152     | EGNJ | 2501kgs  | TAXI_G,TAXI_H               |
 | Invalid data - aircraft        | B747     | EGNJ | 44000kgs |                             |
 | Invalid data - airfield        | A320     | EGKK | 44000kgs |                             |
 | Invalid data - weight          | A320     | EGNJ |          |                             |
 | Invalid data - all             | B747     | EGKK |          |                             |
 