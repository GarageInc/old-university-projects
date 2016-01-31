/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package matrix;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author 7
 */
public class MyMatrixTest {
    
    public MyMatrixTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
    }
    
    @After
    public void tearDown() {
    }

    /**
     * Test of getM method, of class MyMatrix.
     */
    @Test
    public void testGetM() {
        System.out.println("getM");
        MyMatrix instance = null;
        double[][] expResult = null;
        double[][] result = instance.getM();
        assertArrayEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setM method, of class MyMatrix.
     */
    @Test
    public void testSetM() {
        System.out.println("setM");
        double[][] value = null;
        MyMatrix instance = null;
        instance.setM(value);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getW method, of class MyMatrix.
     */
    @Test
    public void testGetW() {
        System.out.println("getW");
        MyMatrix instance = null;
        int expResult = 0;
        int result = instance.getW();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setW method, of class MyMatrix.
     */
    @Test
    public void testSetW() {
        System.out.println("setW");
        int value = 0;
        MyMatrix instance = null;
        instance.setW(value);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of getH method, of class MyMatrix.
     */
    @Test
    public void testGetH() {
        System.out.println("getH");
        MyMatrix instance = null;
        int expResult = 0;
        int result = instance.getH();
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of setH method, of class MyMatrix.
     */
    @Test
    public void testSetH() {
        System.out.println("setH");
        int value = 0;
        MyMatrix instance = null;
        instance.setH(value);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of GetElement method, of class MyMatrix.
     */
    @Test
    public void testGetElement() {
        System.out.println("GetElement");
        int i = 0;
        int j = 0;
        MyMatrix instance = null;
        double expResult = 0.0;
        double result = instance.GetElement(i, j);
        assertEquals(expResult, result, 0.0);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of SetElement method, of class MyMatrix.
     */
    @Test
    public void testSetElement() {
        System.out.println("SetElement");
        double value = 0.0;
        int i = 0;
        int j = 0;
        MyMatrix instance = null;
        instance.SetElement(value, i, j);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of OpAddition method, of class MyMatrix.
     */
    @Test
    public void testOpAddition() {
        System.out.println("OpAddition");
        MyMatrix a = null;
        MyMatrix b = null;
        MyMatrix expResult = null;
        MyMatrix result = MyMatrix.OpAddition(a, b);
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of OpMultiply method, of class MyMatrix.
     */
    @Test
    public void testOpMultiply() {
        System.out.println("OpMultiply");
        MyMatrix A = null;
        MyMatrix B = null;
        MyMatrix expResult = null;
        MyMatrix result = MyMatrix.OpMultiply(A, B);
        assertEquals(expResult, result);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of Determinant method, of class MyMatrix.
     */
    @Test
    public void testDeterminant() {
        System.out.println("Determinant");
        int n = 0;
        MyMatrix instance = null;
        double expResult = 0.0;
        double result = instance.Determinant(n);
        assertEquals(expResult, result, 0.0);
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }

    /**
     * Test of Print method, of class MyMatrix.
     */
    @Test
    public void testPrint() {
        System.out.println("Print");
        MyMatrix instance = null;
        instance.Print();
        // TODO review the generated test code and remove the default call to fail.
        fail("The test case is a prototype.");
    }
    
}
