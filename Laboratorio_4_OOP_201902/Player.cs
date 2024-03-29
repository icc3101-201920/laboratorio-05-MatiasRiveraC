﻿using Laboratorio_4_OOP_201902.Cards;
using Laboratorio_4_OOP_201902.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio_4_OOP_201902
{
    public class Player
    {
        //Constantes
        private const int LIFE_POINTS = 2;
        private const int START_ATTACK_POINTS = 0;

        //Static
        private static int idCounter;

        //Atributos
        private int id;
        private int lifePoints;
        private int attackPoints;
        private Deck deck;
        private Hand hand;
        private Board board;
        private SpecialCard captain;

        //Constructor
        public Player()
        {
            LifePoints = LIFE_POINTS;
            AttackPoints = START_ATTACK_POINTS;
            Deck = new Deck();
            Hand = new Hand();
            Id = idCounter++;
        }

        //Propiedades
        public int Id { get => id; set => id = value; }
        public int LifePoints
        {
            get
            {
                return this.lifePoints;
            }
            set
            {
                this.lifePoints = value;
            }
        }
        public int AttackPoints
        {
            get
            {
                return this.attackPoints;
            }
            set
            {
                this.attackPoints = value;
            }
        }
        public Deck Deck
        {
            get
            {
                return this.deck;
            }
            set
            {
                this.deck = value;
            }
        }
        public Hand Hand
        {
            get
            {
                return this.hand;
            }
            set
            {
                this.hand = value;
            }
        }
        public Board Board
        {
            get
            {
                return this.board;
            }
            set
            {
                this.board = value;
            }
        }
        public SpecialCard Captain
        {
            get
            {
                return this.captain;
            }
            set
            {
                this.captain = value;
            }
        }

        //Metodos
        public void DrawCard(int cardId = 0)
        {
            if (deck.Cards[cardId].Type == EnumType.melee || deck.Cards[cardId].Type == EnumType.range || deck.Cards[cardId].Type == EnumType.longRange)
            {
                CombatCard card = (CombatCard)deck.Cards[cardId];
                hand.AddCard(new CombatCard(card.Name,card.Type,card.Effect,card.AttackPoints,card.Hero));
            }
            else if(deck.Cards[cardId].Type != EnumType.None)
            {
                SpecialCard card = (SpecialCard)deck.Cards[cardId];
                hand.AddCard(new SpecialCard(card.Name, card.Type,card.Effect));
            }
            
        }
        public void PlayCard(int cardId, EnumType buffRow = EnumType.None)
        {

            if (hand.Cards[cardId].Type == EnumType.melee || hand.Cards[cardId].Type == EnumType.range || hand.Cards[cardId].Type == EnumType.longRange)
            {
                CombatCard card = (CombatCard)hand.Cards[cardId];
                Board.AddCard(new CombatCard(card.Name, card.Type, card.Effect, card.AttackPoints, card.Hero));
            }
            else if (hand.Cards[cardId].Type != EnumType.None)
            {
                SpecialCard card = (SpecialCard)hand.Cards[cardId];
                if (card.Type == EnumType.weather)
                {
                    Board.AddCard(new SpecialCard(card.Name, card.Type, card.Effect));
                }
                else
                {
                    Board.AddCard(new SpecialCard(card.Name, card.Type, card.Effect),id,buffRow);
                }
                
            }
            hand.DestroyCard(cardId);
            /*Realice el mismo procedimiento que en DrawCard, solo que ahora es desde Hand a Board.
              En caso de CombatCard siga el mismo procedimiento, recuerde que el método AddCard de Board requiere el id del usuario.
              En caso de SpecialCard:
                1- Realice los pasos 2.1 y 2.2 de DrawCard
                2- Identifique el tipo de la carta, 
                    2.1- Si es buff:
                        -El metodo AddCard de Board requiere el Id del usuario
                        -El metodo requiere la entrada buff{fila a la que se va a jugar}. Ejemplo buffmelee, este dato viene en el parámetro buffRow.
                    2.2- Si es weather:
                        -El metodo AddCard solo requiere la carta.
                3- Elimine la carta de la mano. 
             */

        }
        public void ChangeCard(int cardId)
        {
            
            if (hand.Cards[cardId].Type == EnumType.melee || hand.Cards[cardId].Type == EnumType.range || hand.Cards[cardId].Type == EnumType.longRange)
            {
                CombatCard card = (CombatCard)hand.Cards[cardId];
                hand.Cards.RemoveAt(cardId);
                Random rnd = new Random();
                int randomCardId = rnd.Next(0, deck.Cards.Count);
                DrawCard(randomCardId);
                deck.Cards.RemoveAt(randomCardId);
                deck.AddCard(new CombatCard(card.Name, card.Type, card.Effect, card.AttackPoints, card.Hero));

            }
            else if (hand.Cards[cardId].Type != EnumType.None)
            {
                SpecialCard card = (SpecialCard)hand.Cards[cardId];
                hand.Cards.RemoveAt(cardId);
                Random rnd = new Random();
                int randomCardId = rnd.Next(0, deck.Cards.Count);
                DrawCard(randomCardId);
                deck.Cards.RemoveAt(randomCardId);
                deck.AddCard(new SpecialCard(card.Name, card.Type, card.Effect));

            }
            
                /* Debe cambiar la carta en la posicion cardId de la mano por una carta aleatoria del mazo.
                    1- Defina si la carta a cambiar de la mano es CombatCard o SpecialCard. Luego (Esto permite cambiar la referencia):
                            1.1- Asigne una variable a la carta a cambiar de la mano, ejemplo, CombatCard card = hand.Cards[cardId]
                            1.2- Cree una CombatCard o SpecialCard (dependiendo del caso) con los valores de la carta de la mano a cambiar.
                    2- Elimine la carta de la mano
                    3- Implemente Random
                    4- Cree una variable que obtenga un numero aleatorio dentro del total de cartas del mazo.
                    5- Obtenga la carta aleatoria del mazo (puede utilizar el método DrawCard) y cree una nueva carta con sus valores. Agreguela a la mano. 
                    6- Elimine la carta aleatoria escogida del mazo.
                    7- Agregue la carta original de la mano al mazo.
                */
          
        }

        public void FirstHand()
        {
            for (int i=0; i<10; i++)
            {
                Random rnd = new Random();
                int randomCardId = rnd.Next(0, deck.Cards.Count);
                DrawCard(randomCardId);
                deck.DestroyCard(randomCardId);
            }
            /*Debe obtener 10 cartas aleatorias del mazo y asignarlas a la mano.
            Utilice el metodo DrawCard con 10 numeros de id aleatorios.
            */
            
        }

        public void ChooseCaptainCard(SpecialCard captainCard)
        {
            Captain = captainCard;
        }

    }
}
