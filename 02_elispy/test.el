(setq res 1)
(setq n 5)
(while (> n 0) (progn (setq res (* res n)) (setq n (- n 1))))
(princ res)